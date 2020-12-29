namespace Clips
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.AutoHide = new System.Windows.Forms.CheckBox();
            this.AutoSizeHeight = new System.Windows.Forms.CheckBox();
            this.OpenAtMouse = new System.Windows.Forms.CheckBox();
            this.Startup = new System.Windows.Forms.CheckBox();
            this.PreviewMaxLines = new System.Windows.Forms.NumericUpDown();
            this.PreviewPopupDelay = new System.Windows.Forms.NumericUpDown();
            this.ClipsLinesPerRow = new System.Windows.Forms.NumericUpDown();
            this.ClipsMaxClips = new System.Windows.Forms.NumericUpDown();
            this.Key = new System.Windows.Forms.TextBox();
            this.PreviewBackColor = new System.Windows.Forms.Panel();
            this.PreviewFontColor = new System.Windows.Forms.Panel();
            this.MenuSelectedColor = new System.Windows.Forms.Panel();
            this.MenuBorderColor = new System.Windows.Forms.Panel();
            this.MenuFontColor = new System.Windows.Forms.Panel();
            this.MenuBackColor = new System.Windows.Forms.Panel();
            this.ClipHeaderColor = new System.Windows.Forms.Panel();
            this.ClipRowColor = new System.Windows.Forms.Panel();
            this.ClipBackColor = new System.Windows.Forms.Panel();
            this.ClipFontColor = new System.Windows.Forms.Panel();
            this.MenuButtonColor = new System.Windows.Forms.Panel();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupOther = new System.Windows.Forms.GroupBox();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbSizes = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTextRows = new System.Windows.Forms.Label();
            this.gbHotkey = new System.Windows.Forms.GroupBox();
            this.Windows = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Shift = new System.Windows.Forms.CheckBox();
            this.Alt = new System.Windows.Forms.CheckBox();
            this.Control = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Preview = new System.Windows.Forms.GroupBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.DarkTheme = new System.Windows.Forms.RadioButton();
            this.LightTheme = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewMaxLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPopupDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipsLinesPerRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipsMaxClips)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupOther.SuspendLayout();
            this.groupPreview.SuspendLayout();
            this.gbSizes.SuspendLayout();
            this.gbHotkey.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Preview.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoHide
            // 
            this.AutoHide.AutoSize = true;
            this.AutoHide.Location = new System.Drawing.Point(6, 83);
            this.AutoHide.Name = "AutoHide";
            this.AutoHide.Size = new System.Drawing.Size(71, 17);
            this.AutoHide.TabIndex = 20;
            this.AutoHide.Text = "Auto hide";
            this.toolTip1.SetToolTip(this.AutoHide, "Check to auto hide clips window after clicking a clip.");
            this.AutoHide.UseVisualStyleBackColor = true;
            // 
            // AutoSizeHeight
            // 
            this.AutoSizeHeight.AutoSize = true;
            this.AutoSizeHeight.Location = new System.Drawing.Point(6, 60);
            this.AutoSizeHeight.Name = "AutoSizeHeight";
            this.AutoSizeHeight.Size = new System.Drawing.Size(101, 17);
            this.AutoSizeHeight.TabIndex = 2;
            this.AutoSizeHeight.Text = "Auto size height";
            this.toolTip1.SetToolTip(this.AutoSizeHeight, "Check to automatically size clips height to the number of clips.");
            this.AutoSizeHeight.UseVisualStyleBackColor = true;
            // 
            // OpenAtMouse
            // 
            this.OpenAtMouse.AutoSize = true;
            this.OpenAtMouse.Location = new System.Drawing.Point(6, 39);
            this.OpenAtMouse.Name = "OpenAtMouse";
            this.OpenAtMouse.Size = new System.Drawing.Size(98, 17);
            this.OpenAtMouse.TabIndex = 1;
            this.OpenAtMouse.Text = "Open at mouse";
            this.toolTip1.SetToolTip(this.OpenAtMouse, "Check to automatically display clips at mouse.");
            this.OpenAtMouse.UseVisualStyleBackColor = true;
            // 
            // Startup
            // 
            this.Startup.AutoSize = true;
            this.Startup.Location = new System.Drawing.Point(6, 18);
            this.Startup.Name = "Startup";
            this.Startup.Size = new System.Drawing.Size(112, 17);
            this.Startup.TabIndex = 0;
            this.Startup.Text = "Start automatically";
            this.toolTip1.SetToolTip(this.Startup, "Check to automatically start clips.");
            this.Startup.UseVisualStyleBackColor = true;
            // 
            // PreviewMaxLines
            // 
            this.PreviewMaxLines.Location = new System.Drawing.Point(115, 18);
            this.PreviewMaxLines.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.PreviewMaxLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PreviewMaxLines.Name = "PreviewMaxLines";
            this.PreviewMaxLines.Size = new System.Drawing.Size(62, 20);
            this.PreviewMaxLines.TabIndex = 0;
            this.toolTip1.SetToolTip(this.PreviewMaxLines, "Set the max number of lines to preview.");
            this.PreviewMaxLines.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PreviewPopupDelay
            // 
            this.PreviewPopupDelay.Location = new System.Drawing.Point(115, 45);
            this.PreviewPopupDelay.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.PreviewPopupDelay.Name = "PreviewPopupDelay";
            this.PreviewPopupDelay.Size = new System.Drawing.Size(62, 20);
            this.PreviewPopupDelay.TabIndex = 1;
            this.toolTip1.SetToolTip(this.PreviewPopupDelay, "Delay in milliseconds before preview pops up.");
            // 
            // ClipsLinesPerRow
            // 
            this.ClipsLinesPerRow.Location = new System.Drawing.Point(115, 18);
            this.ClipsLinesPerRow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ClipsLinesPerRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ClipsLinesPerRow.Name = "ClipsLinesPerRow";
            this.ClipsLinesPerRow.Size = new System.Drawing.Size(62, 20);
            this.ClipsLinesPerRow.TabIndex = 0;
            this.toolTip1.SetToolTip(this.ClipsLinesPerRow, "Set the number of rows of text per clip to display in list.");
            this.ClipsLinesPerRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ClipsMaxClips
            // 
            this.ClipsMaxClips.Location = new System.Drawing.Point(115, 45);
            this.ClipsMaxClips.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ClipsMaxClips.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ClipsMaxClips.Name = "ClipsMaxClips";
            this.ClipsMaxClips.Size = new System.Drawing.Size(62, 20);
            this.ClipsMaxClips.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ClipsMaxClips, "Set  the max number of clips to save.");
            this.ClipsMaxClips.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Key
            // 
            this.Key.Location = new System.Drawing.Point(63, 19);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(80, 20);
            this.Key.TabIndex = 0;
            this.toolTip1.SetToolTip(this.Key, "Press key to define as a hotkey.");
            this.Key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Key_KeyDown);
            this.Key.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_KeyPress);
            // 
            // PreviewBackColor
            // 
            this.PreviewBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewBackColor.Location = new System.Drawing.Point(6, 51);
            this.PreviewBackColor.Name = "PreviewBackColor";
            this.PreviewBackColor.Size = new System.Drawing.Size(26, 27);
            this.PreviewBackColor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.PreviewBackColor, "Click to set color.");
            this.PreviewBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // PreviewFontColor
            // 
            this.PreviewFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewFontColor.Location = new System.Drawing.Point(6, 18);
            this.PreviewFontColor.Name = "PreviewFontColor";
            this.PreviewFontColor.Size = new System.Drawing.Size(26, 27);
            this.PreviewFontColor.TabIndex = 0;
            this.toolTip1.SetToolTip(this.PreviewFontColor, "Click to set color.");
            this.PreviewFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // MenuSelectedColor
            // 
            this.MenuSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuSelectedColor.Location = new System.Drawing.Point(6, 117);
            this.MenuSelectedColor.Name = "MenuSelectedColor";
            this.MenuSelectedColor.Size = new System.Drawing.Size(26, 27);
            this.MenuSelectedColor.TabIndex = 3;
            this.toolTip1.SetToolTip(this.MenuSelectedColor, "Click to set color.");
            this.MenuSelectedColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // MenuBorderColor
            // 
            this.MenuBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuBorderColor.Location = new System.Drawing.Point(6, 84);
            this.MenuBorderColor.Name = "MenuBorderColor";
            this.MenuBorderColor.Size = new System.Drawing.Size(26, 27);
            this.MenuBorderColor.TabIndex = 2;
            this.toolTip1.SetToolTip(this.MenuBorderColor, "Click to set color.");
            this.MenuBorderColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // MenuFontColor
            // 
            this.MenuFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuFontColor.Location = new System.Drawing.Point(6, 18);
            this.MenuFontColor.Name = "MenuFontColor";
            this.MenuFontColor.Size = new System.Drawing.Size(26, 27);
            this.MenuFontColor.TabIndex = 0;
            this.toolTip1.SetToolTip(this.MenuFontColor, "Click to set color.");
            this.MenuFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // MenuBackColor
            // 
            this.MenuBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuBackColor.Location = new System.Drawing.Point(6, 51);
            this.MenuBackColor.Name = "MenuBackColor";
            this.MenuBackColor.Size = new System.Drawing.Size(26, 27);
            this.MenuBackColor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.MenuBackColor, "Click to set color.");
            this.MenuBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // ClipHeaderColor
            // 
            this.ClipHeaderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClipHeaderColor.Location = new System.Drawing.Point(10, 18);
            this.ClipHeaderColor.Name = "ClipHeaderColor";
            this.ClipHeaderColor.Size = new System.Drawing.Size(26, 27);
            this.ClipHeaderColor.TabIndex = 0;
            this.toolTip1.SetToolTip(this.ClipHeaderColor, "Click to set color.");
            this.ClipHeaderColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // ClipRowColor
            // 
            this.ClipRowColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClipRowColor.Location = new System.Drawing.Point(10, 117);
            this.ClipRowColor.Name = "ClipRowColor";
            this.ClipRowColor.Size = new System.Drawing.Size(26, 27);
            this.ClipRowColor.TabIndex = 3;
            this.toolTip1.SetToolTip(this.ClipRowColor, "Click to set color.");
            this.ClipRowColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // ClipBackColor
            // 
            this.ClipBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClipBackColor.Location = new System.Drawing.Point(10, 84);
            this.ClipBackColor.Name = "ClipBackColor";
            this.ClipBackColor.Size = new System.Drawing.Size(26, 27);
            this.ClipBackColor.TabIndex = 2;
            this.toolTip1.SetToolTip(this.ClipBackColor, "Click to set color.");
            this.ClipBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // ClipFontColor
            // 
            this.ClipFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClipFontColor.Location = new System.Drawing.Point(10, 51);
            this.ClipFontColor.Name = "ClipFontColor";
            this.ClipFontColor.Size = new System.Drawing.Size(26, 27);
            this.ClipFontColor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ClipFontColor, "Click to set color.");
            this.ClipFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // MenuButtonColor
            // 
            this.MenuButtonColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuButtonColor.Location = new System.Drawing.Point(119, 18);
            this.MenuButtonColor.Name = "MenuButtonColor";
            this.MenuButtonColor.Size = new System.Drawing.Size(26, 27);
            this.MenuButtonColor.TabIndex = 20;
            this.toolTip1.SetToolTip(this.MenuButtonColor, "Click to set color.");
            this.MenuButtonColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // dlgColor
            // 
            this.dlgColor.AnyColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Cancel);
            this.groupBox2.Controls.Add(this.OK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(5, 414);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 54);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(253, 14);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 34);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(172, 14);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 34);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupOther);
            this.panel1.Controls.Add(this.groupPreview);
            this.panel1.Controls.Add(this.gbSizes);
            this.panel1.Controls.Add(this.gbHotkey);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 197);
            this.panel1.TabIndex = 6;
            // 
            // groupOther
            // 
            this.groupOther.Controls.Add(this.AutoHide);
            this.groupOther.Controls.Add(this.AutoSizeHeight);
            this.groupOther.Controls.Add(this.OpenAtMouse);
            this.groupOther.Controls.Add(this.Startup);
            this.groupOther.Location = new System.Drawing.Point(311, 3);
            this.groupOther.Name = "groupOther";
            this.groupOther.Size = new System.Drawing.Size(183, 109);
            this.groupOther.TabIndex = 5;
            this.groupOther.TabStop = false;
            // 
            // groupPreview
            // 
            this.groupPreview.Controls.Add(this.label4);
            this.groupPreview.Controls.Add(this.PreviewMaxLines);
            this.groupPreview.Controls.Add(this.label7);
            this.groupPreview.Controls.Add(this.PreviewPopupDelay);
            this.groupPreview.Location = new System.Drawing.Point(311, 118);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Size = new System.Drawing.Size(183, 73);
            this.groupPreview.TabIndex = 7;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Preview";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Max # of lines";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Delay (ms)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbSizes
            // 
            this.gbSizes.Controls.Add(this.label6);
            this.gbSizes.Controls.Add(this.lblTextRows);
            this.gbSizes.Controls.Add(this.ClipsLinesPerRow);
            this.gbSizes.Controls.Add(this.ClipsMaxClips);
            this.gbSizes.Location = new System.Drawing.Point(6, 92);
            this.gbSizes.Name = "gbSizes";
            this.gbSizes.Size = new System.Drawing.Size(299, 99);
            this.gbSizes.TabIndex = 6;
            this.gbSizes.TabStop = false;
            this.gbSizes.Text = "Clips";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Max # of clips";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTextRows
            // 
            this.lblTextRows.AutoSize = true;
            this.lblTextRows.Location = new System.Drawing.Point(13, 22);
            this.lblTextRows.Margin = new System.Windows.Forms.Padding(0);
            this.lblTextRows.Name = "lblTextRows";
            this.lblTextRows.Size = new System.Drawing.Size(70, 13);
            this.lblTextRows.TabIndex = 15;
            this.lblTextRows.Text = "Lines per row";
            this.lblTextRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbHotkey
            // 
            this.gbHotkey.Controls.Add(this.Key);
            this.gbHotkey.Controls.Add(this.Windows);
            this.gbHotkey.Controls.Add(this.label2);
            this.gbHotkey.Controls.Add(this.Shift);
            this.gbHotkey.Controls.Add(this.Alt);
            this.gbHotkey.Controls.Add(this.Control);
            this.gbHotkey.Controls.Add(this.label1);
            this.gbHotkey.Location = new System.Drawing.Point(6, 3);
            this.gbHotkey.Name = "gbHotkey";
            this.gbHotkey.Size = new System.Drawing.Size(299, 83);
            this.gbHotkey.TabIndex = 4;
            this.gbHotkey.TabStop = false;
            this.gbHotkey.Text = "Popup Hotkey";
            // 
            // Windows
            // 
            this.Windows.AutoSize = true;
            this.Windows.Location = new System.Drawing.Point(209, 45);
            this.Windows.Name = "Windows";
            this.Windows.Size = new System.Drawing.Size(70, 17);
            this.Windows.TabIndex = 4;
            this.Windows.Text = "Windows";
            this.Windows.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Modifier";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Shift
            // 
            this.Shift.AutoSize = true;
            this.Shift.Location = new System.Drawing.Point(162, 45);
            this.Shift.Name = "Shift";
            this.Shift.Size = new System.Drawing.Size(47, 17);
            this.Shift.TabIndex = 3;
            this.Shift.Text = "Shift";
            this.Shift.UseVisualStyleBackColor = true;
            // 
            // Alt
            // 
            this.Alt.AutoSize = true;
            this.Alt.Location = new System.Drawing.Point(123, 45);
            this.Alt.Name = "Alt";
            this.Alt.Size = new System.Drawing.Size(38, 17);
            this.Alt.TabIndex = 2;
            this.Alt.Text = "Alt";
            this.Alt.UseVisualStyleBackColor = true;
            // 
            // Control
            // 
            this.Control.AutoSize = true;
            this.Control.Location = new System.Drawing.Point(63, 45);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(59, 17);
            this.Control.TabIndex = 1;
            this.Control.Text = "Control";
            this.Control.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Key";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.panel4);
            this.groupBox6.Controls.Add(this.panel5);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(5, 202);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(500, 212);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Colors";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Preview);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 49);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(494, 160);
            this.panel4.TabIndex = 1;
            // 
            // Preview
            // 
            this.Preview.Controls.Add(this.lblBackColor);
            this.Preview.Controls.Add(this.lblFontColor);
            this.Preview.Controls.Add(this.PreviewBackColor);
            this.Preview.Controls.Add(this.PreviewFontColor);
            this.Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Preview.Location = new System.Drawing.Point(348, 0);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(146, 160);
            this.Preview.TabIndex = 2;
            this.Preview.TabStop = false;
            this.Preview.Text = "Preview";
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(38, 58);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(65, 13);
            this.lblBackColor.TabIndex = 7;
            this.lblBackColor.Text = "Background";
            this.lblBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFontColor.Location = new System.Drawing.Point(38, 25);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(28, 13);
            this.lblFontColor.TabIndex = 6;
            this.lblFontColor.Text = "Font";
            this.lblFontColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.MenuButtonColor);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.MenuSelectedColor);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.MenuBorderColor);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.MenuFontColor);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.MenuBackColor);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(151, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 160);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Menu";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(151, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "Button";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(38, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Selected";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Border";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(38, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Font";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Background";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.ClipHeaderColor);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.ClipRowColor);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.ClipBackColor);
            this.groupBox4.Controls.Add(this.ClipFontColor);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(151, 160);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Clips";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(42, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Header";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Row";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Background";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(42, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Font";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.DarkTheme);
            this.panel5.Controls.Add(this.LightTheme);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(494, 33);
            this.panel5.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Preset colors";
            // 
            // DarkTheme
            // 
            this.DarkTheme.AutoSize = true;
            this.DarkTheme.Location = new System.Drawing.Point(152, 7);
            this.DarkTheme.Name = "DarkTheme";
            this.DarkTheme.Size = new System.Drawing.Size(48, 17);
            this.DarkTheme.TabIndex = 1;
            this.DarkTheme.TabStop = true;
            this.DarkTheme.Text = "Dark";
            this.DarkTheme.UseVisualStyleBackColor = true;
            this.DarkTheme.Click += new System.EventHandler(this.DarkTheme_Click);
            // 
            // LightTheme
            // 
            this.LightTheme.AutoSize = true;
            this.LightTheme.Location = new System.Drawing.Point(103, 7);
            this.LightTheme.Name = "LightTheme";
            this.LightTheme.Size = new System.Drawing.Size(48, 17);
            this.LightTheme.TabIndex = 0;
            this.LightTheme.TabStop = true;
            this.LightTheme.Text = "Light";
            this.LightTheme.UseVisualStyleBackColor = true;
            this.LightTheme.Click += new System.EventHandler(this.LightTheme_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 473);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clips Configuration";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormConfig_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewMaxLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPopupDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipsLinesPerRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipsMaxClips)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupOther.ResumeLayout(false);
            this.groupOther.PerformLayout();
            this.groupPreview.ResumeLayout(false);
            this.groupPreview.PerformLayout();
            this.gbSizes.ResumeLayout(false);
            this.gbSizes.PerformLayout();
            this.gbHotkey.ResumeLayout(false);
            this.gbHotkey.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.Preview.ResumeLayout(false);
            this.Preview.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.Button OK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupOther;
        public System.Windows.Forms.CheckBox AutoHide;
        public System.Windows.Forms.CheckBox AutoSizeHeight;
        public System.Windows.Forms.CheckBox OpenAtMouse;
        public System.Windows.Forms.CheckBox Startup;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown PreviewMaxLines;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown PreviewPopupDelay;
        private System.Windows.Forms.GroupBox gbSizes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTextRows;
        public System.Windows.Forms.NumericUpDown ClipsLinesPerRow;
        public System.Windows.Forms.NumericUpDown ClipsMaxClips;
        private System.Windows.Forms.GroupBox gbHotkey;
        public System.Windows.Forms.TextBox Key;
        public System.Windows.Forms.CheckBox Windows;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox Shift;
        public System.Windows.Forms.CheckBox Alt;
        public System.Windows.Forms.CheckBox Control;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox Preview;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblFontColor;
        public System.Windows.Forms.Panel PreviewBackColor;
        public System.Windows.Forms.Panel PreviewFontColor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.Panel MenuSelectedColor;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Panel MenuBorderColor;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Panel MenuFontColor;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Panel MenuBackColor;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Panel ClipHeaderColor;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Panel ClipRowColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Panel ClipBackColor;
        public System.Windows.Forms.Panel ClipFontColor;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton DarkTheme;
        private System.Windows.Forms.RadioButton LightTheme;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Panel MenuButtonColor;
    }
}