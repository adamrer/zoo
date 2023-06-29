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
            lVystup = new Label();
            lTyp_navstevniku = new Label();
            cbTyp_navstevniku = new ComboBox();
            bSmazLog = new Button();
            bSmazOut = new Button();
            tbFiltr = new TextBox();
            label1 = new Label();
            lPocetStan = new Label();
            lPocetObc = new Label();
            lTrpelivost = new Label();
            lHlad = new Label();
            lJezeni = new Label();
            lPrichod = new Label();
            numPocetStan_max = new NumericUpDown();
            numPocetStan_min = new NumericUpDown();
            numPocetObc_max = new NumericUpDown();
            numPocetObc_min = new NumericUpDown();
            numTrpelivost_max = new NumericUpDown();
            numTrpelivost_min = new NumericUpDown();
            lPocet_max1 = new Label();
            lPocet_min1 = new Label();
            numHlad_max = new NumericUpDown();
            numHlad_min = new NumericUpDown();
            numJezeni_max = new NumericUpDown();
            numJezeni_min = new NumericUpDown();
            lVMinutach = new Label();
            label9 = new Label();
            numSeed = new NumericUpDown();
            tbDo = new TextBox();
            tbOd = new TextBox();
            tbPrichod = new TextBox();
            lPocitani = new Label();
            checkData = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numPocet_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_krok).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocet_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocetStan_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocetStan_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocetObc_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPocetObc_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTrpelivost_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTrpelivost_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHlad_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHlad_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numJezeni_max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numJezeni_min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSeed).BeginInit();
            SuspendLayout();
            // 
            // bStart
            // 
            bStart.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            bStart.Location = new Point(811, 34);
            bStart.Name = "bStart";
            bStart.Size = new Size(211, 306);
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
            lOd.Location = new Point(198, 37);
            lOd.Name = "lOd";
            lOd.Size = new Size(29, 20);
            lOd.TabIndex = 2;
            lOd.Text = "Od";
            // 
            // lDo
            // 
            lDo.AutoSize = true;
            lDo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lDo.Location = new Point(301, 37);
            lDo.Name = "lDo";
            lDo.Size = new Size(29, 20);
            lDo.TabIndex = 3;
            lDo.Text = "Do";
            // 
            // lPocet_navstevniku
            // 
            lPocet_navstevniku.AutoSize = true;
            lPocet_navstevniku.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPocet_navstevniku.Location = new Point(23, 128);
            lPocet_navstevniku.Name = "lPocet_navstevniku";
            lPocet_navstevniku.Size = new Size(140, 20);
            lPocet_navstevniku.TabIndex = 4;
            lPocet_navstevniku.Text = "Počet návštěvníků:";
            // 
            // lPocet_min
            // 
            lPocet_min.AutoSize = true;
            lPocet_min.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_min.Location = new Point(209, 102);
            lPocet_min.Name = "lPocet_min";
            lPocet_min.Size = new Size(34, 20);
            lPocet_min.TabIndex = 5;
            lPocet_min.Text = "Min";
            // 
            // lPocet_max
            // 
            lPocet_max.AutoSize = true;
            lPocet_max.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_max.Location = new Point(322, 102);
            lPocet_max.Name = "lPocet_max";
            lPocet_max.Size = new Size(37, 20);
            lPocet_max.TabIndex = 6;
            lPocet_max.Text = "Max";
            // 
            // lPocet_krok
            // 
            lPocet_krok.AutoSize = true;
            lPocet_krok.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_krok.Location = new Point(433, 102);
            lPocet_krok.Name = "lPocet_krok";
            lPocet_krok.Size = new Size(39, 20);
            lPocet_krok.TabIndex = 7;
            lPocet_krok.Text = "Krok";
            // 
            // tbLog
            // 
            tbLog.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            tbLog.Location = new Point(23, 372);
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.Size = new Size(643, 256);
            tbLog.TabIndex = 8;
            tbLog.Text = "";
            // 
            // tbVystup
            // 
            tbVystup.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            tbVystup.Location = new Point(674, 372);
            tbVystup.Name = "tbVystup";
            tbVystup.ReadOnly = true;
            tbVystup.Size = new Size(348, 256);
            tbVystup.TabIndex = 9;
            tbVystup.Text = "";
            // 
            // bVstup_soubor
            // 
            bVstup_soubor.Location = new Point(23, 292);
            bVstup_soubor.Name = "bVstup_soubor";
            bVstup_soubor.Size = new Size(217, 25);
            bVstup_soubor.TabIndex = 10;
            bVstup_soubor.Text = "Vybrat soubor se vtupními daty";
            bVstup_soubor.UseVisualStyleBackColor = true;
            bVstup_soubor.Click += bVstup_soubor_Click;
            // 
            // lVstup_soubor
            // 
            lVstup_soubor.AutoSize = true;
            lVstup_soubor.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lVstup_soubor.Location = new Point(23, 320);
            lVstup_soubor.Name = "lVstup_soubor";
            lVstup_soubor.Size = new Size(188, 20);
            lVstup_soubor.TabIndex = 11;
            lVstup_soubor.Text = "Žádný soubor nebyl vybrán";
            // 
            // numPocet_min
            // 
            numPocet_min.Location = new Point(198, 125);
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
            numPocet_krok.Location = new Point(422, 125);
            numPocet_krok.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocet_krok.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocet_krok.Name = "numPocet_krok";
            numPocet_krok.Size = new Size(62, 23);
            numPocet_krok.TabIndex = 13;
            numPocet_krok.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numPocet_max
            // 
            numPocet_max.Location = new Point(309, 125);
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
            checkLog.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            checkLog.Location = new Point(23, 349);
            checkLog.Name = "checkLog";
            checkLog.Size = new Size(54, 24);
            checkLog.TabIndex = 15;
            checkLog.Text = "Log";
            checkLog.UseVisualStyleBackColor = true;
            // 
            // lVystup
            // 
            lVystup.AutoSize = true;
            lVystup.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lVystup.Location = new Point(674, 349);
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
            cbTyp_navstevniku.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTyp_navstevniku.FormattingEnabled = true;
            cbTyp_navstevniku.Items.AddRange(new object[] { "0 - Další v listu", "1 - Stejné patro", "2 - Nejmenší fronta", "3 - Nejméně v patře", "Všichni" });
            cbTyp_navstevniku.Location = new Point(198, 74);
            cbTyp_navstevniku.Name = "cbTyp_navstevniku";
            cbTyp_navstevniku.Size = new Size(145, 23);
            cbTyp_navstevniku.TabIndex = 20;
            // 
            // bSmazLog
            // 
            bSmazLog.Location = new Point(23, 628);
            bSmazLog.Name = "bSmazLog";
            bSmazLog.Size = new Size(91, 23);
            bSmazLog.TabIndex = 21;
            bSmazLog.Text = "Vymazat log";
            bSmazLog.UseVisualStyleBackColor = true;
            bSmazLog.Click += bSmazLog_Click;
            // 
            // bSmazOut
            // 
            bSmazOut.Location = new Point(674, 628);
            bSmazOut.Name = "bSmazOut";
            bSmazOut.Size = new Size(103, 23);
            bSmazOut.TabIndex = 22;
            bSmazOut.Text = "Vymazat výstup";
            bSmazOut.UseVisualStyleBackColor = true;
            bSmazOut.Click += bSmazOut_Click;
            // 
            // tbFiltr
            // 
            tbFiltr.Location = new Point(139, 349);
            tbFiltr.Name = "tbFiltr";
            tbFiltr.Size = new Size(100, 23);
            tbFiltr.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(91, 349);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 24;
            label1.Text = "Filtr:";
            // 
            // lPocetStan
            // 
            lPocetStan.AutoSize = true;
            lPocetStan.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPocetStan.Location = new Point(23, 157);
            lPocetStan.Name = "lPocetStan";
            lPocetStan.Size = new Size(122, 20);
            lPocetStan.TabIndex = 25;
            lPocetStan.Text = "Počet stanovišť:";
            // 
            // lPocetObc
            // 
            lPocetObc.AutoSize = true;
            lPocetObc.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPocetObc.Location = new Point(23, 187);
            lPocetObc.Name = "lPocetObc";
            lPocetObc.Size = new Size(137, 20);
            lPocetObc.TabIndex = 26;
            lPocetObc.Text = "Počet občerstvení:";
            // 
            // lTrpelivost
            // 
            lTrpelivost.AutoSize = true;
            lTrpelivost.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lTrpelivost.Location = new Point(497, 128);
            lTrpelivost.Name = "lTrpelivost";
            lTrpelivost.Size = new Size(82, 20);
            lTrpelivost.TabIndex = 27;
            lTrpelivost.Text = "Trpělivost:";
            // 
            // lHlad
            // 
            lHlad.AutoSize = true;
            lHlad.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lHlad.Location = new Point(497, 158);
            lHlad.Name = "lHlad";
            lHlad.Size = new Size(45, 20);
            lHlad.TabIndex = 28;
            lHlad.Text = "Hlad:";
            // 
            // lJezeni
            // 
            lJezeni.AutoSize = true;
            lJezeni.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lJezeni.Location = new Point(497, 190);
            lJezeni.Name = "lJezeni";
            lJezeni.Size = new Size(117, 20);
            lJezeni.TabIndex = 29;
            lJezeni.Text = "Rychlost jezení:";
            // 
            // lPrichod
            // 
            lPrichod.AutoSize = true;
            lPrichod.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPrichod.Location = new Point(23, 219);
            lPrichod.Name = "lPrichod";
            lPrichod.Size = new Size(66, 20);
            lPrichod.TabIndex = 30;
            lPrichod.Text = "Příchod:";
            // 
            // numPocetStan_max
            // 
            numPocetStan_max.Location = new Point(309, 154);
            numPocetStan_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocetStan_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetStan_max.Name = "numPocetStan_max";
            numPocetStan_max.Size = new Size(62, 23);
            numPocetStan_max.TabIndex = 34;
            numPocetStan_max.Value = new decimal(new int[] { 26, 0, 0, 0 });
            numPocetStan_max.ValueChanged += numPocetStan_max_ValueChanged;
            // 
            // numPocetStan_min
            // 
            numPocetStan_min.Location = new Point(198, 154);
            numPocetStan_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocetStan_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetStan_min.Name = "numPocetStan_min";
            numPocetStan_min.Size = new Size(62, 23);
            numPocetStan_min.TabIndex = 33;
            numPocetStan_min.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetStan_min.ValueChanged += numPocetStan_min_ValueChanged;
            // 
            // numPocetObc_max
            // 
            numPocetObc_max.Location = new Point(309, 187);
            numPocetObc_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocetObc_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetObc_max.Name = "numPocetObc_max";
            numPocetObc_max.Size = new Size(62, 23);
            numPocetObc_max.TabIndex = 38;
            numPocetObc_max.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numPocetObc_max.ValueChanged += numPocetObc_max_ValueChanged;
            // 
            // numPocetObc_min
            // 
            numPocetObc_min.Location = new Point(198, 187);
            numPocetObc_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numPocetObc_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetObc_min.Name = "numPocetObc_min";
            numPocetObc_min.Size = new Size(62, 23);
            numPocetObc_min.TabIndex = 37;
            numPocetObc_min.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numPocetObc_min.ValueChanged += numPocetObc_min_ValueChanged;
            // 
            // numTrpelivost_max
            // 
            numTrpelivost_max.Location = new Point(715, 125);
            numTrpelivost_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numTrpelivost_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTrpelivost_max.Name = "numTrpelivost_max";
            numTrpelivost_max.Size = new Size(62, 23);
            numTrpelivost_max.TabIndex = 46;
            numTrpelivost_max.Value = new decimal(new int[] { 240, 0, 0, 0 });
            numTrpelivost_max.ValueChanged += numTrpelivost_max_ValueChanged;
            // 
            // numTrpelivost_min
            // 
            numTrpelivost_min.Location = new Point(618, 125);
            numTrpelivost_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numTrpelivost_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTrpelivost_min.Name = "numTrpelivost_min";
            numTrpelivost_min.Size = new Size(62, 23);
            numTrpelivost_min.TabIndex = 45;
            numTrpelivost_min.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numTrpelivost_min.ValueChanged += numTrpelivost_min_ValueChanged;
            // 
            // lPocet_max1
            // 
            lPocet_max1.AutoSize = true;
            lPocet_max1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_max1.Location = new Point(726, 102);
            lPocet_max1.Name = "lPocet_max1";
            lPocet_max1.Size = new Size(37, 20);
            lPocet_max1.TabIndex = 44;
            lPocet_max1.Text = "Max";
            // 
            // lPocet_min1
            // 
            lPocet_min1.AutoSize = true;
            lPocet_min1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lPocet_min1.Location = new Point(632, 102);
            lPocet_min1.Name = "lPocet_min1";
            lPocet_min1.Size = new Size(34, 20);
            lPocet_min1.TabIndex = 43;
            lPocet_min1.Text = "Min";
            // 
            // numHlad_max
            // 
            numHlad_max.Location = new Point(715, 155);
            numHlad_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numHlad_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHlad_max.Name = "numHlad_max";
            numHlad_max.Size = new Size(62, 23);
            numHlad_max.TabIndex = 50;
            numHlad_max.Value = new decimal(new int[] { 300, 0, 0, 0 });
            numHlad_max.ValueChanged += numHlad_max_ValueChanged;
            // 
            // numHlad_min
            // 
            numHlad_min.Location = new Point(618, 155);
            numHlad_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numHlad_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHlad_min.Name = "numHlad_min";
            numHlad_min.Size = new Size(62, 23);
            numHlad_min.TabIndex = 49;
            numHlad_min.Value = new decimal(new int[] { 120, 0, 0, 0 });
            numHlad_min.ValueChanged += numHlad_min_ValueChanged;
            // 
            // numJezeni_max
            // 
            numJezeni_max.Location = new Point(715, 187);
            numJezeni_max.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numJezeni_max.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numJezeni_max.Name = "numJezeni_max";
            numJezeni_max.Size = new Size(62, 23);
            numJezeni_max.TabIndex = 54;
            numJezeni_max.Value = new decimal(new int[] { 90, 0, 0, 0 });
            numJezeni_max.ValueChanged += numJezeni_max_ValueChanged;
            // 
            // numJezeni_min
            // 
            numJezeni_min.Location = new Point(618, 187);
            numJezeni_min.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numJezeni_min.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numJezeni_min.Name = "numJezeni_min";
            numJezeni_min.Size = new Size(62, 23);
            numJezeni_min.TabIndex = 53;
            numJezeni_min.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numJezeni_min.ValueChanged += numJezeni_min_ValueChanged;
            // 
            // lVMinutach
            // 
            lVMinutach.AutoSize = true;
            lVMinutach.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lVMinutach.Location = new Point(651, 73);
            lVMinutach.Name = "lVMinutach";
            lVMinutach.Size = new Size(91, 20);
            lVMinutach.TabIndex = 56;
            lVMinutach.Text = "(v minutách)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(497, 292);
            label9.Name = "label9";
            label9.Size = new Size(82, 20);
            label9.TabIndex = 57;
            label9.Text = "RNG seed:";
            // 
            // numSeed
            // 
            numSeed.Location = new Point(618, 289);
            numSeed.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numSeed.Name = "numSeed";
            numSeed.Size = new Size(124, 23);
            numSeed.TabIndex = 58;
            numSeed.Value = new decimal(new int[] { 12345, 0, 0, 0 });
            // 
            // tbDo
            // 
            tbDo.Location = new Point(336, 34);
            tbDo.Name = "tbDo";
            tbDo.Size = new Size(62, 23);
            tbDo.TabIndex = 17;
            tbDo.Text = "19:00";
            // 
            // tbOd
            // 
            tbOd.Location = new Point(233, 34);
            tbOd.Name = "tbOd";
            tbOd.Size = new Size(62, 23);
            tbOd.TabIndex = 16;
            tbOd.Text = "9:00";
            // 
            // tbPrichod
            // 
            tbPrichod.Location = new Point(309, 216);
            tbPrichod.Name = "tbPrichod";
            tbPrichod.Size = new Size(62, 23);
            tbPrichod.TabIndex = 55;
            tbPrichod.Text = "13:00";
            tbPrichod.TextChanged += textBox1_TextChanged;
            // 
            // lPocitani
            // 
            lPocitani.AutoSize = true;
            lPocitani.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lPocitani.Location = new Point(873, 343);
            lPocitani.Name = "lPocitani";
            lPocitani.Size = new Size(79, 25);
            lPocitani.TabIndex = 59;
            lPocitani.Text = "POČÍTÁ";
            lPocitani.Visible = false;
            // 
            // checkData
            // 
            checkData.AutoSize = true;
            checkData.Location = new Point(738, 353);
            checkData.Name = "checkData";
            checkData.Size = new Size(78, 19);
            checkData.TabIndex = 60;
            checkData.Text = "Čistě data";
            checkData.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 674);
            Controls.Add(checkData);
            Controls.Add(lPocitani);
            Controls.Add(numSeed);
            Controls.Add(label9);
            Controls.Add(lVMinutach);
            Controls.Add(tbPrichod);
            Controls.Add(numJezeni_max);
            Controls.Add(numJezeni_min);
            Controls.Add(numHlad_max);
            Controls.Add(numHlad_min);
            Controls.Add(numTrpelivost_max);
            Controls.Add(numTrpelivost_min);
            Controls.Add(lPocet_max1);
            Controls.Add(lPocet_min1);
            Controls.Add(numPocetObc_max);
            Controls.Add(numPocetObc_min);
            Controls.Add(numPocetStan_max);
            Controls.Add(numPocetStan_min);
            Controls.Add(lPrichod);
            Controls.Add(lJezeni);
            Controls.Add(lHlad);
            Controls.Add(lTrpelivost);
            Controls.Add(lPocetObc);
            Controls.Add(lPocetStan);
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
            ((System.ComponentModel.ISupportInitialize)numPocetStan_max).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPocetStan_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPocetObc_max).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPocetObc_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTrpelivost_max).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTrpelivost_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHlad_max).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHlad_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numJezeni_max).EndInit();
            ((System.ComponentModel.ISupportInitialize)numJezeni_min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSeed).EndInit();
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
        private Label lVystup;
        private Label lTyp_navstevniku;
        private ComboBox cbTyp_navstevniku;
        private Button bSmazLog;
        private Button bSmazOut;
        private TextBox tbFiltr;
        private Label label1;
        private Label lPocetStan;
        private Label lPocetObc;
        private Label lTrpelivost;
        private Label lHlad;
        private Label lJezeni;
        private Label lPrichod;
        private NumericUpDown numPocetStan_max;
        private NumericUpDown numPocetStan_min;
        private NumericUpDown numPocetObc_max;
        private NumericUpDown numPocetObc_min;
        private NumericUpDown numTrpelivost_max;
        private NumericUpDown numTrpelivost_min;
        private Label lPocet_max1;
        private Label lPocet_min1;
        private NumericUpDown numHlad_max;
        private NumericUpDown numHlad_min;
        private NumericUpDown numJezeni_max;
        private NumericUpDown numJezeni_min;
        private Label lVMinutach;
        private Label label9;
        private NumericUpDown numSeed;
        private TextBox tbDo;
        private TextBox tbOd;
        private TextBox tbPrichod;
        private Label lPocitani;
        private CheckBox checkData;
    }
}