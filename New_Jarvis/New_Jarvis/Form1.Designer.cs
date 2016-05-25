namespace New_Jarvis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.fieldView = new System.Windows.Forms.DataGridView();
            this.valueButton = new MetroFramework.Controls.MetroButton();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.valueButton2 = new MetroFramework.Controls.MetroButton();
            this.resetButton2 = new MetroFramework.Controls.MetroButton();
            this.helpBox = new MetroFramework.Controls.MetroComboBox();
            this.helpButton = new MetroFramework.Controls.MetroButton();
            this.blackValue = new MetroFramework.Controls.MetroLabel();
            this.whiteValue = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldView)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 63);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(434, 470);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.fieldView);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(426, 428);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "MAIN";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // fieldView
            // 
            this.fieldView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldView.Location = new System.Drawing.Point(3, 3);
            this.fieldView.Name = "fieldView";
            this.fieldView.RowTemplate.Height = 23;
            this.fieldView.Size = new System.Drawing.Size(419, 394);
            this.fieldView.TabIndex = 2;
            // 
            // valueButton
            // 
            this.valueButton.Location = new System.Drawing.Point(459, 104);
            this.valueButton.Name = "valueButton";
            this.valueButton.Size = new System.Drawing.Size(102, 23);
            this.valueButton.TabIndex = 1;
            this.valueButton.Text = "Value for Black";
            this.valueButton.UseSelectable = true;
            this.valueButton.Click += new System.EventHandler(this.valueButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(374, 34);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset Field";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // valueButton2
            // 
            this.valueButton2.Location = new System.Drawing.Point(459, 133);
            this.valueButton2.Name = "valueButton2";
            this.valueButton2.Size = new System.Drawing.Size(102, 23);
            this.valueButton2.TabIndex = 3;
            this.valueButton2.Text = "Value for White";
            this.valueButton2.UseSelectable = true;
            this.valueButton2.Click += new System.EventHandler(this.valueButton2_Click);
            // 
            // resetButton2
            // 
            this.resetButton2.Location = new System.Drawing.Point(459, 162);
            this.resetButton2.Name = "resetButton2";
            this.resetButton2.Size = new System.Drawing.Size(102, 23);
            this.resetButton2.TabIndex = 4;
            this.resetButton2.Text = "Reset Value";
            this.resetButton2.UseSelectable = true;
            this.resetButton2.Click += new System.EventHandler(this.resetButton2_Click);
            // 
            // helpBox
            // 
            this.helpBox.FormattingEnabled = true;
            this.helpBox.ItemHeight = 23;
            this.helpBox.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.helpBox.Location = new System.Drawing.Point(272, 28);
            this.helpBox.Name = "helpBox";
            this.helpBox.Size = new System.Drawing.Size(96, 29);
            this.helpBox.TabIndex = 5;
            this.helpBox.UseSelectable = true;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(459, 34);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(102, 23);
            this.helpButton.TabIndex = 6;
            this.helpButton.Text = "Help Me Jarvis";
            this.helpButton.UseSelectable = true;
            this.helpButton.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // blackValue
            // 
            this.blackValue.AutoSize = true;
            this.blackValue.Location = new System.Drawing.Point(463, 451);
            this.blackValue.Name = "blackValue";
            this.blackValue.Size = new System.Drawing.Size(90, 19);
            this.blackValue.TabIndex = 7;
            this.blackValue.Text = "B\'s total score";
            // 
            // whiteValue
            // 
            this.whiteValue.AutoSize = true;
            this.whiteValue.Location = new System.Drawing.Point(463, 479);
            this.whiteValue.Name = "whiteValue";
            this.whiteValue.Size = new System.Drawing.Size(95, 19);
            this.whiteValue.TabIndex = 8;
            this.whiteValue.Text = "W\'s total score";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 550);
            this.Controls.Add(this.whiteValue);
            this.Controls.Add(this.blackValue);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.helpBox);
            this.Controls.Add(this.resetButton2);
            this.Controls.Add(this.valueButton2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.valueButton);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "Form1";
            this.Text = "Jarvis for Gomoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fieldView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.DataGridView fieldView;
        private MetroFramework.Controls.MetroButton valueButton;
        private MetroFramework.Controls.MetroButton resetButton;
        private MetroFramework.Controls.MetroButton valueButton2;
        private MetroFramework.Controls.MetroButton resetButton2;
        private MetroFramework.Controls.MetroComboBox helpBox;
        private MetroFramework.Controls.MetroButton helpButton;
        private MetroFramework.Controls.MetroLabel blackValue;
        private MetroFramework.Controls.MetroLabel whiteValue;
    }
}

