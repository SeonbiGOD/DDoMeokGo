using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            int k = 0;

            while (k != -1)
            {
                admin.playGame();
                admin.printField();
                k = admin.getInput();
            }
                   
        }
    }
}
