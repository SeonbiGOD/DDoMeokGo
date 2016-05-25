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
        private ActionManager am;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // make field
            for (int i = 0; i < 15; i++)
            {
                fieldView.Columns.Add("", "");
                fieldView.Columns[i].Width = 25;
                if (i != 14)
                {
                    fieldView.Rows.Add();
                    fieldView.Rows[i].Height = 25;
                }
            }

            // default = "Black"
            helpBox.SelectedIndex = 0;

            // for Action
            am = new ActionManager(fieldView);
        }

        // Calculate Value for Black
        private void valueButton_Click(object sender, EventArgs e)
        {   
            am.valueFunctionForBlack();
        }

        // Calculate Value for White
        private void valueButton2_Click(object sender, EventArgs e)
        {
            am.valueFunctionForWhite();
        }

        // Reset Field
        private void resetButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    fieldView[i, j].Value = null;
                }
            }
            am.resetField();
        }

        // Reset Value
        private void resetButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (fieldView[i, j].Value != null)
                    {
                        if (String.Compare(fieldView[i, j].Value.ToString(), "B") != 0 && String.Compare(fieldView[i, j].Value.ToString(), "W") != 0)
                        {
                            fieldView[i, j].Value = null;
                        }
                    }
                }
            }
            am.resetValue();
        }

        // Help Me Jarvis Button
        private void metroButton1_Click(object sender, EventArgs e)
        {
            int ret;
            ret = am.helpMeJarvis(helpBox.SelectedIndex, blackValue, whiteValue);
            if (ret == -999)
            {
                // Resign
                MetroMessageBox.Show(this, "Resign.");
            }
            else if (ret == 999)
            {
                // Resign
                MetroMessageBox.Show(this, "Win.");
            }
        }
    }
}
