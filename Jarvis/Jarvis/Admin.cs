using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Jarvis
{
    class Admin
    {
        private char[,] field = new char[15, 15];
        private string source = @"./Data";
        private string root = @"./Data";
        
        public Admin()
        {
            // init
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    field[i, j] = 'x';
                }
            }
        }

        public int getInput()
        {
            string input;
            string[] words;
            int x, y;
            char who;
            Console.Write("Input : ");
            input = Console.ReadLine();
            words = input.Split(',');
            x = Convert.ToInt32(words[0]);
            y = Convert.ToInt32(words[1]);
            who = Convert.ToChar(words[2][1]);

            int value = -2;
            string s_pos = x.ToString() + "," + y.ToString() + "," + value.ToString();
            string sDirPath = source + "\\" + s_pos;
            DirectoryInfo di = new DirectoryInfo(sDirPath);

            if (who == 'B' || who == 'W' || who == 'b' || who == 'w')
            {
                field[x - 1, y - 1] = who;
                if (!di.Exists)
                {
                    di.Create();
                }
                
                return 0;
            }

            return -1;
        }

        public void printField()
        {
            int cnt = 1;

            // pint
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (cnt < 15)
                    {
                        Console.Write(field[i, j].ToString() + " ");
                        cnt += 1;
                    }
                    else
                    {
                        Console.WriteLine(field[i, j].ToString());
                        cnt = 1;
                    }
                }
            }
        }

        public void playGame()
        {
            string[] dirs, pos;
            string data, backup, max_backup = "";
            int x, y, value;
            int max = -1;
            int res_x = -1;
            int res_y = -1;

            // Data로부터 가장 높은 가치를 가진 수를 찾는다.
            // (내가 두었던 수 중에서 높은 가치를 가지는 수)
            dirs = Directory.GetDirectories(source);
            foreach (string dir in dirs)
            {
                data = dir.Replace(source + "\\", "");
                backup = data;
                Console.WriteLine(data);

                pos = data.Split(',');
                x = Convert.ToInt32(pos[0]);
                y = Convert.ToInt32(pos[1]);
                value = Convert.ToInt32(pos[2]);
                if (max < value && field[x-1, y-1] != 'x')
                {
                    max = value;
                    res_x = x;
                    res_y = y;
                    max_backup = backup;
                }
            }

            // Data가 존재하지 않는 경우
            if (max == -1)
            {
                // 앞 전까지의 Data들로 부터 할 수 있는 최적의 Action을 찾는다.
                // root 부터 last(source)까지 내가 두었던 수들과 상대방의 수들을 통해 최적의 Action을 찾는다.
                // (1) 내가 두었던 수들로부터 찾은 최적의 Action과 가치를 계산한다.
                string temp = actionPolicy(false);

                // (2) 상대가 두었던 수들로부터 찾은 최적의 Action과 가치를 계산한다.
                temp = actionPolicy(true);

                // (1),(2)로 부터 가장 높은 가치를 갖는 Action을 선택한다.

                // 해당 Action이 오목 룰에 위배되는지 체크해야한다.(ex - 흑일 때 33 불가능)
            }
            // 존재하는 경우
            else
            {
                // 기보에서 가장 높은 가치의 수를 선택하여 두도록 한다.
                // 동률의 수들이 여러가지 인 경우는 고려해야할 사항이다.
                field[res_x - 1, res_y - 1] = 'B';
                source = source + "\\" + max_backup;
            }
        }

        // root ~ source 경로에서 가장 높은 가치를 가진 패턴을 찾는다.
        public string actionPolicy(bool type)
        {
            int max = -1;
            string ret = "";
            // root, source(last) 

            // 내가 두었던 수들로부터 찾는다.
            if (!type)
            {
                
            }
            // 상대가 두었던 수들로부터 찾는다.
            else
            {

            }
            return ret;
        }

        // 43, 33 등등.. 패턴 인식 함수 필요
        // (1) 현재 상황에서 둘 수 있는 패턴들을 찾는다.
        // (2) 가장 높은 가치를 지닐 수 있는 패턴을 만드는 수를 리턴.
        public int valueFunction()
        {
            return 0;
        }
    }
}
