using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Resources;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace DDoMeokGo
{
    public partial class Form1 : Form
    {
        private class Sys
        {
            public static ProcessStartInfo cmdProcessInfo;
            public static Process cmdProcess;
            public static Bitmap screen;
            public static int delay = 400;
            public static Thread mainThread;
            public static bool problem;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            UpdateDeviceList();
            Cursor = Cursors.Default;
        }

        // 블루스택 연결(ADB 이용)
        private void UpdateDeviceList()
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            button1.Text = "갱신중";
            button1.Enabled = false;

            try
            {
                string ADBFilename = SearchADBFilename();

                if (!string.IsNullOrWhiteSpace(ADBFilename))
                {
                    Sys.cmdProcessInfo = new ProcessStartInfo()
                    {
                        FileName = ADBFilename,
                        Arguments = "devices",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };
                    Sys.cmdProcess = new Process() { StartInfo = Sys.cmdProcessInfo };
                    Sys.cmdProcess.Start();
                    string result = Sys.cmdProcess.StandardOutput.ReadToEnd();
                    string[] deviceList = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < deviceList.Length; i++)
                    {
                        if (deviceList[i].Contains("device") && !deviceList[i].Contains("devices"))
                        {
                            string deviceSerialNumber = deviceList[i].Remove(deviceList[i].IndexOf("\t"));
                            comboBox1.Items.Add(deviceSerialNumber);
                        }
                    }
                    Sys.cmdProcess.WaitForExit();
                    Sys.cmdProcess.Close();
                }
                else
                {
                    MessageBox.Show(this, "ADB파일을 찾을 수 없습니다.\n블루스택을 실행해주세요.",
                        "갱신 실패", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.ToString(), "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            button1.Text = "새로고침";
            button1.Enabled = true;
            comboBox1.SelectedIndex = comboBox1.Items.Count == 0 ? -1 : 0;
        }

        // Process 리스트를 통해 ADB 추출
        private string SearchADBFilename()
        {
            string ADBFilename;

            List<Process> processes = new List<Process>();
            processes.AddRange(Process.GetProcessesByName("HD-Frontend"));
            if (processes.Count > 0)
            {
                string ADBName = null;
                switch (processes[0].ProcessName)
                {
                    case "HD-Frontend":
                        ADBName = "HD-Adb";
                        break;
                }
                ADBFilename = processes[0].Modules[0].FileName;
                ADBFilename = string.Format("{0}{1}.exe", ADBFilename.Remove(ADBFilename.LastIndexOf("\\") + 1), ADBName);
                return ADBFilename;
            }
            return null;
        }

        // Command 명령 함수
        public void Command(string cmd)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                return;

            lock (Sys.cmdProcess)
            {
                Sys.cmdProcessInfo.Arguments = string.Format("-s {0} {1}", comboBox1.Text, cmd);
                Sys.cmdProcess.Start();
                Sys.cmdProcess.WaitForExit();
                Sys.cmdProcess.Close();
            }
        }

        // Command 클릭 명령
        public void TargetClick(int x, int y)
        {
            Command(string.Format("shell input tap {0} {1}", x, y));
        }

        // 캡쳐함수
        public Bitmap TargetCapture()
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                return null;

            Bitmap image = null;
            lock (Sys.cmdProcess)
            {
                string imageFilename = string.Format(@"{0}\Pictures\DDoMeokGo_Capture.png", Environment.GetEnvironmentVariable("HomePath"));
                Command("shell screencap -p /sdcard/DDoMeokGo_Capture.png");
                Command(string.Format(@"pull /sdcard/DDoMeokGo_Capture.png {0}", imageFilename));
                Command("shell rm /sdcard/DDoMeokGo_Capture.png");

                if (!File.Exists(imageFilename))
                    return null;
                StreamReader reader = new StreamReader(imageFilename);
                image = (Bitmap)Image.FromStream(reader.BaseStream);
                reader.Close();
                File.Delete(imageFilename);
            }
            return image;
        }

        // 모니터링 - 화면 캡쳐
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show(this, "대상기기를 선택해주세요.", "캡쳐 실패", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            button2.Enabled = false;

            Bitmap image = TargetCapture();

            if (tabControl1.SelectedTab.Name.Equals("tabPage1"))
                pictureBox1.Image = image;

            //Directory.CreateDirectory(@"Screenshots\");
            //image.Save(string.Format(@"Screenshots\{0}.png", DateTime.Now.ToString("yyyyMMdd_HHmmss")));

            button2.Enabled = true;
        }

        // Log
        public void Log(string log)
        {
            dataGridView1.Rows.Insert(0, 1);
            dataGridView1.Rows[0].Cells[0].Value = log;
        }

        // Bitmap 서치 함수
        public Rectangle BitmapSearch(Bitmap bigBmp, Bitmap smallBmp, double tolerance)
        {
            BitmapData smallData = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bigData = bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        // Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                // With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            // We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            // Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        // If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }

                        // If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }
                    if (matchFound) break;
                    pBig += bigOffset;
                }
            }
            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);
            return location;
        }

        // Work 함수들 ( 이미지 탐색 후 해당 이미지 존재하면 클릭 )
        public bool Work(int mode, string name)
        {
            return Work(mode, name, -1, -1, true, 0.1);
        }
        public bool Work(int mode, string name, int clickX, int clickY)
        {
            return Work(mode, name, clickX, clickY, true, 0.1);
        }
        public bool Work(int mode, string name, int clickX, int clickY, bool capture)
        {
            return Work(mode, name, clickX, clickY, capture, 0.1);
        }
        public bool Work(int mode, string name, int clickX, int clickY, bool capture, double tolerance)
        {
            bool matched = false;
            if (Sys.problem)
                mode = mode | 1;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string strBaseName = string.Format("{0}.Properties.Resources", assembly.GetName().Name);
            ResourceManager rm = new ResourceManager(strBaseName, assembly);

            for (int i = 0; i < 5 * 60 * 1000 / (Sys.delay == 0 ? 1 : Sys.delay); i++)
            {
                if (((mode & 1) == 0 && !Sys.problem) || matched)
                    Thread.Sleep(Sys.delay);

                if (!Sys.problem && capture)
                    Sys.screen = TargetCapture();
                Rectangle location = BitmapSearch(Sys.screen, (Bitmap)rm.GetObject(name), tolerance);
                Log(string.Format("{0} {1}", name, location == Rectangle.Empty ? "찾기 " + i : "발견"));

                if (location != Rectangle.Empty)
                {
                    if (tabControl1.SelectedTab.Name.Equals("tabPage1"))
                    {
                        Graphics g = Graphics.FromImage(Sys.screen);
                        g.DrawRectangle(new Pen(Color.Red, 5), location);
                        pictureBox1.Image = new Bitmap(Sys.screen.Width, Sys.screen.Height, g);
                    }
                    Sys.problem = false;
                    if (clickX >= 0 && clickY >= 0 && (!matched || (i % 20 == 0)))
                        TargetClick((clickX == 0) ? location.X + location.Width : clickX,
                            (clickY == 0) ? location.Y + location.Height : clickY);
                    if ((mode & 2) == 2)
                        return true;
                }
                else
                {
                    if (tabControl1.SelectedTab.Name.Equals("tabPage1"))
                        pictureBox1.Image = Sys.screen;
                    if (matched)
                        return true;
                    if ((mode & 1) == 1)
                        return false;
                }
            }
            Do("초기화");
            return false;
        }

        // Do 함수( 게임 상의 행동 )
        public void Do(string type)
        {
            switch (type)
            {
                /*
                 *  (삭제될수도있는 함수)
                    ex) 행동력 구매
                */
            }
        }

        // Run
        public void Run()
        {
            //Sys.problem = true;
            Sys.screen = TargetCapture();

            /*
                게임 진행 및 기보 저장 부분이 구현되어야함.
            */
        }
    }
}
