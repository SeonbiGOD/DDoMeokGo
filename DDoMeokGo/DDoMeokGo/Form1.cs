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
using Microsoft.Win32;

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
            public static Rectangle sResult;
        }

        private class ImgPos
        {
            public static int x, y;
        }

        private bool isRunning;

        public Form1()
        {
            isRunning = false;
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

        // 모니터링 - 캡쳐
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show(this, "대상기기를 선택해주세요.", "캡쳐 실패", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            button2.Enabled = false;
            pictureBox1.Image = TargetCapture();
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

        // 이미지 서칭 함수(BitmapSearch 함수 이용) mode 가 1인 경우 -> 서칭 성공 한 부분 클릭
        public bool ImageSearch(string name, int mode)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string strBaseName = string.Format("{0}.Properties.Resources", assembly.GetName().Name);
            ResourceManager rm = new ResourceManager(strBaseName, assembly);

            // tolerance
            double tolerance = 0.1;

            // 현재 화면
            Sys.screen = TargetCapture();

            // 이미지 서치
            Sys.sResult = new Rectangle();
            Sys.sResult = BitmapSearch(Sys.screen, (Bitmap)rm.GetObject(name), tolerance);
            Log(string.Format("{0} {1}", name, Sys.sResult == Rectangle.Empty ? "서칭 실패" : "서칭 성공"));
            if (Sys.sResult.IsEmpty)
            {
                return false;
            }
            else
            {
                if (mode == 1)
                {
                    // 윈도우 좌표 -> 블루스택 좌표
                    int x, y;
                    x = Sys.sResult.X + Sys.sResult.Width;
                    y = Sys.sResult.Y + Sys.sResult.Height;
                    y *= 2;

                    TargetClick(x, y);
                }
                return true;
            }
        }

        // Run
        public void Run()
        {
            bool res;
            int x, y;

            #region 진입
            // 공지사항 -> 확인
            while (true)
            {
                res = ImageSearch("확인", 1);
                if (res)
                {
                    break;
                }
            }
            #endregion

            #region 대국
            // 일반 대국 신청 
            
            #endregion
        }

        // 시작 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show(this, "대상기기를 선택해주세요.", "실행 실패", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.isRunning = false;                
                return;
            }

            // 
            if (isRunning == false)
            {
                button3.Text = "중지";
                isRunning = true;
                Log("시작");
                Sys.mainThread = new Thread(Run);
                Sys.mainThread.Start();
            }
            else
            {
                isRunning = false;
                button3.Text = "시작";
                Sys.mainThread.Abort();
                Log("중지");
            }
        }

        // 해상도 설정
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                string pname = p.ProcessName;
                if (pname.StartsWith("HD-"))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch
                    {
                        MessageBox.Show("관리자 권한으로 실행해주세요.");
                        break;
                    }
                }
            }
            RegistryKey reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\BlueStacks\Guests\Android\FrameBuffer\0");
            reg.SetValue("GuestWidth", 1440);
            reg.SetValue("GuestHeight", 900);
            reg.SetValue("WindowWidth", 720);
            reg.SetValue("WindowHeight", 450);

            MessageBox.Show("해상도 설정 완료. 블루스택을 재실행 하신 후 이용해주세요.");
        }
    }
}
