using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Jarvis
{
    public partial class Form1 : MetroForm
    {
        private char[,] field = new char[15, 15];
        private bool turn = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // init field
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    field[i, j] = 'x';
                }
            }

            // make field view
            for (int i = 0; i < 15; i++)
            {
                fieldView.Columns.Add("", "");
                fieldView.Columns[i].Width = 25;
                fieldView.Rows.Add();
                fieldView.Rows[i].Height = 25;
            }
        }

        private void Cell_Changed(object sender, DataGridViewCellEventArgs e)
        {
            if (!turn)
            {
                field[e.RowIndex, e.ColumnIndex] = 'B';
                turn = !turn;
            }
            else
            {
                turn = !turn;
                field[e.RowIndex, e.ColumnIndex] = 'W';
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            string result = "";

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (cnt < 15)
                    {
                        result += field[i, j].ToString();
                        cnt += 1;
                    }
                    else
                    {
                        result += '\n';
                        cnt = 0;
                        continue;
                    }
                }
            }

            MetroMessageBox.Show(this, result, "Current Field", 300);
        }
    }
}
