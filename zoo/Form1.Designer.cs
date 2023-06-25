namespace zoo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bStart = new Button();
            lOteviraci_doba = new Label();
            lOd = new Label();
            lDo = new Label();
            lPocet_navstevniku = new Label();
            lPocet_min = new Label();
            lPocet_max = new Label();
            lPocet_krok = new Label();
            tbLog = new RichTextBox();
            tbVystup = new RichTextBox();
            bVstup_soubor = new Button();
            lVstup_soubor = new Label();
            numPocet_min = new NumericUpDown();
            numPocet_krok = new NumericUpDown();
            numPocet_max = new NumericUpDown();
            checkLog = new CheckBox();
            tbOd = new TextBox();
            tbDo = new TextBox();
            lVystup = new Label();
            lTyp_navstevniku = new Label();
            cbTyp_navstevniku = new ComboBox();
            bSmazLog = new Button();
            bSmazOut = new Button();
            tbFiltr = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)numPocet_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_krok).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_max).BeginInit();
            SuspendLayout();
            // 
            // bStart
            // 
            bStart.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            bStart.Location = new Point(662, 34);
            bStart.Name = "bStart";
            bStart.Size = new Size(115, 69);
            bStart.TabIndex = 0;
            bStart.Text = "START";
            bStart.UseVisualStyleBackColor = true;
            bStart.Click += bStart_Click;
            // 
            // lOteviraci_doba
            // 
            lOteviraci_doba.AutoSize = true;
            lOteviraci_doba.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lOteviraci_doba.Location = new Point(23, 37);
            lOteviraci_doba.Name = "lOteviraci_doba";
            lOteviraci_doba.Size = new Size(114, 20);
            lOteviraci_doba.TabIndex = 1;
            lOteviraci_doba.Text = "Otevírací doba:";
            // 
            // lOd
            // 
            lOd.AutoSize = true;
            lOd.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lOd.Location = new Point(139, 37);
            lOd.Name = "lOd";
            lOd.Size = new Size(29, 20);
            lOd.TabIndex = 2;
            lOd.Text = "Od";
            // 
            // lDo
            // 
            lDo.AutoSize = true;
            lDo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lDo.Location = new Point(258, 37);
            lDo.Name = "lDo";
            lDo.Size = new Size(29, 20);
            lDo.TabIndex = 3;
            lDo.Text = "Do";
            // 
            // lPocet_navstevniku
            // 
            lPocet_navstevniku.AutoSize = true;
            lPocet_navstevniku.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPocet_navstevniku.Location = new Point(23, 113);
            lPocet_navstevniku.Name = "lPocet_navstevniku";
            lPocet_navstevniku.Size = new Size(140, 20);
            lPocet_navstevniku.TabIndex = 4;
            lPocet_navstevniku.Text = "Počet návštěvníků:";
            // 
            // lPocet_min
            // 
            lPocet_min.AutoSize = true;
            lPocet_min.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_min.Location = new Point(158, 113);
            lPocet_min.Name = "lPocet_min";
            lPocet_min.Size = new Size(34, 20);
            lPocet_min.TabIndex = 5;
            lPocet_min.Text = "Min";
            // 
            // lPocet_max
            // 
            lPocet_max.AutoSize = true;
            lPocet_max.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_max.Location = new Point(266, 112);
            lPocet_max.Name = "lPocet_max";
            lPocet_max.Size = new Size(37, 20);
            lPocet_max.TabIndex = 6;
            lPocet_max.Text = "Max";
            // 
            // lPocet_krok
            // 
            lPocet_krok.AutoSize = true;
            lPocet_krok.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_krok.Location = new Point(377, 112);
            lPocet_krok.Name = "lPocet_krok";
            lPocet_krok.Size = new Size(39, 20);
            lPocet_krok.TabIndex = 7;
            lPocet_krok.Text = "Krok";
            // 
            // tbLog
            // 
            tbLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbLog.Location = new Point(26, 200);
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.Size = new Size(557, 216);
            tbLog.TabIndex = 8;
            tbLog.Text = "";
            // 
            // tbVystup
            // 
            tbVystup.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbVystup.Location = new Point(606, 170);
            tbVystup.Name = "tbVystup";
            tbVystup.ReadOnly = true;
            tbVystup.Size = new Size(256, 256);
            tbVystup.TabIndex = 9;
            tbVystup.Text = "";
            // 
            // bVstup_soubor
            // 
            bVstup_soubor.Location = new Point(23, 142);
            bVstup_soubor.Name = "bVstup_soubor";
            bVstup_soubor.Size = new Size(217, 25);
            bVstup_soubor.TabIndex = 10;
            bVstup_soubor.Text = "Vybrat soubor se vtupními daty";
            bVstup_soubor.UseVisualStyleBackColor = true;
            bVstup_soubor.Click += bVstup_soubor_Click;
            // 
            // lVstup_soubor
            // 
            lVstup_soubor.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lVstup_soubor.Location = new Point(26, 170);
            lVstup_soubor.Name = "lVstup_soubor";
            lVstup_soubor.Size = new Size(328, 20);
            lVstup_soubor.TabIndex = 11;
            lVstup_soubor.Text = "E:\\MFF\\programování 2\\zoo\\zoo\\vstup.txt";
            // 
            // numPocet_min
            // 
            numPocet_min.Location = new Point(198, 110);
            numPocet_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocet_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocet_min.Name = "numPocet_min";
            numPocet_min.Size = new Size(62, 23);
            numPocet_min.TabIndex = 12;
            numPocet_min.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numPocet_min.ValueChanged += numPocet_min_ValueChanged;
            // 
            // numPocet_krok
            // 
            numPocet_krok.Location = new Point(422, 110);
            numPocet_krok.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocet_krok.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocet_krok.Name = "numPocet_krok";
            numPocet_krok.Size = new Size(62, 23);
            numPocet_krok.TabIndex = 13;
            numPocet_krok.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numPocet_max
            // 
            numPocet_max.Location = new Point(309, 110);
            numPocet_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocet_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocet_max.Name = "numPocet_max";
            numPocet_max.Size = new Size(62, 23);
            numPocet_max.TabIndex = 14;
            numPocet_max.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numPocet_max.ValueChanged += numPocet_max_ValueChanged;
            // 
            // checkLog
            // 
            checkLog.AutoSize = true;
            checkLog.Checked = true;
            checkLog.CheckState = CheckState.Checked;
            checkLog.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkLog.Location = new Point(530, 170);
            checkLog.Name = "checkLog";
            checkLog.Size = new Size(53, 24);
            checkLog.TabIndex = 15;
            checkLog.Text = "Log";
            checkLog.UseVisualStyleBackColor = true;
            // 
            // tbOd
            // 
            tbOd.Location = new Point(168, 34);
            tbOd.Name = "tbOd";
            tbOd.Size = new Size(84, 23);
            tbOd.TabIndex = 16;
            tbOd.Text = "9:00";
            // 
            // tbDo
            // 
            tbDo.Location = new Point(286, 34);
            tbDo.Name = "tbDo";
            tbDo.Size = new Size(84, 23);
            tbDo.TabIndex = 17;
            tbDo.Text = "19:00";
            // 
            // lVystup
            // 
            lVystup.AutoSize = true;
            lVystup.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lVystup.Location = new Point(606, 147);
            lVystup.Name = "lVystup";
            lVystup.Size = new Size(58, 20);
            lVystup.TabIndex = 18;
            lVystup.Text = "Výstup";
            // 
            // lTyp_navstevniku
            // 
            lTyp_navstevniku.AutoSize = true;
            lTyp_navstevniku.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lTyp_navstevniku.Location = new Point(23, 77);
            lTyp_navstevniku.Name = "lTyp_navstevniku";
            lTyp_navstevniku.Size = new Size(126, 20);
            lTyp_navstevniku.TabIndex = 19;
            lTyp_navstevniku.Text = "Typ návštěvníků:";
            // 
            // cbTyp_navstevniku
            // 
            cbTyp_navstevniku.FormattingEnabled = true;
            cbTyp_navstevniku.Items.AddRange(new object[] { "0 - První z listu", "1 - Nejmenší fronta", "2 - Hlad", "3 - Stejné patro" });
            cbTyp_navstevniku.Location = new Point(158, 74);
            cbTyp_navstevniku.Name = "cbTyp_navstevniku";
            cbTyp_navstevniku.Size = new Size(145, 23);
            cbTyp_navstevniku.TabIndex = 20;
            // 
            // bSmazLog
            // 
            bSmazLog.Location = new Point(26, 422);
            bSmazLog.Name = "bSmazLog";
            bSmazLog.Size = new Size(91, 23);
            bSmazLog.TabIndex = 21;
            bSmazLog.Text = "Vymazat log";
            bSmazLog.UseVisualStyleBackColor = true;
            bSmazLog.Click += bSmazLog_Click;
            // 
            // bSmazOut
            // 
            bSmazOut.Location = new Point(606, 426);
            bSmazOut.Name = "bSmazOut";
            bSmazOut.Size = new Size(103, 23);
            bSmazOut.TabIndex = 22;
            bSmazOut.Text = "Vymazat výstup";
            bSmazOut.UseVisualStyleBackColor = true;
            bSmazOut.Click += bSmazOut_Click;
            // 
            // tbFiltr
            // 
            tbFiltr.Location = new Point(408, 170);
            tbFiltr.Name = "tbFiltr";
            tbFiltr.Size = new Size(100, 23);
            tbFiltr.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(360, 170);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 24;
            label1.Text = "Filtr:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 484);
            Controls.Add(label1);
            Controls.Add(tbFiltr);
            Controls.Add(bSmazOut);
            Controls.Add(bSmazLog);
            Controls.Add(cbTyp_navstevniku);
            Controls.Add(lTyp_navstevniku);
            Controls.Add(lVystup);
            Controls.Add(tbDo);
            Controls.Add(tbOd);
            Controls.Add(checkLog);
            Controls.Add(numPocet_max);
            Controls.Add(numPocet_krok);
            Controls.Add(numPocet_min);
            Controls.Add(lVstup_soubor);
            Controls.Add(bVstup_soubor);
            Controls.Add(tbVystup);
            Controls.Add(tbLog);
            Controls.Add(lPocet_krok);
            Controls.Add(lPocet_max);
            Controls.Add(lPocet_min);
            Controls.Add(lPocet_navstevniku);
            Controls.Add(lDo);
            Controls.Add(lOd);
            Controls.Add(lOteviraci_doba);
            Controls.Add(bStart);
            Name = "Form1";
            Text = "Zoo";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numPocet_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_krok).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_max).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bStart;
        private Label lOteviraci_doba;
        private Label lOd;
        private Label lDo;
        private Label lPocet_navstevniku;
        private Label lPocet_min;
        private Label lPocet_max;
        private Label lPocet_krok;
        private RichTextBox tbLog;
        private RichTextBox tbVystup;
        private Button bVstup_soubor;
        private Label lVstup_soubor;
        private NumericUpDown numPocet_min;
        private NumericUpDown numPocet_krok;
        private NumericUpDown numPocet_max;
        private CheckBox checkLog;
        private TextBox tbOd;
        private TextBox tbDo;
        private Label lVystup;
        private Label lTyp_navstevniku;
        private ComboBox cbTyp_navstevniku;
        private Button bSmazLog;
        private Button bSmazOut;
        private TextBox tbFiltr;
        private Label label1;
    }
}