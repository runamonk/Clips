namespace Clips
{
    partial class formSettings
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
            this.gbHotkey = new System.Windows.Forms.GroupBox();
            this.textHotkey = new System.Windows.Forms.TextBox();
            this.chkWin = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShift = new System.Windows.Forms.CheckBox();
            this.chkAlt = new System.Windows.Forms.CheckBox();
            this.chkControl = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSizes = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlClipsRowColor = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlClipsBackColor = new System.Windows.Forms.Panel();
            this.pnlClipsFontColor = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTextRows = new System.Windows.Forms.Label();
            this.nudClipsLinesPerRow = new System.Windows.Forms.NumericUpDown();
            this.nudMaxClips = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.nudPreviewPopupDelay = new System.Windows.Forms.NumericUpDown();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.pnlPreviewBackColor = new System.Windows.Forms.Panel();
            this.pnlPreviewFontColor = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPreviewMaxLines = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlHeaderColor = new System.Windows.Forms.Panel();
            this.gbHotkey.SuspendLayout();
            this.gbSizes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClipsLinesPerRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewPopupDelay)).BeginInit();
            this.groupPreview.SuspendLayout();
            this.gbColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewMaxLines)).BeginInit();
            this.SuspendLayout();
            // 
            // gbHotkey
            // 
            this.gbHotkey.Controls.Add(this.textHotkey);
            this.gbHotkey.Controls.Add(this.chkWin);
            this.gbHotkey.Controls.Add(this.label2);
            this.gbHotkey.Controls.Add(this.chkShift);
            this.gbHotkey.Controls.Add(this.chkAlt);
            this.gbHotkey.Controls.Add(this.chkControl);
            this.gbHotkey.Controls.Add(this.label1);
            this.gbHotkey.Location = new System.Drawing.Point(5, 8);
            this.gbHotkey.Name = "gbHotkey";
            this.gbHotkey.Size = new System.Drawing.Size(223, 93);
            this.gbHotkey.TabIndex = 0;
            this.gbHotkey.TabStop = false;
            this.gbHotkey.Text = "Popup Hotkey";
            // 
            // textHotkey
            // 
            this.textHotkey.Location = new System.Drawing.Point(63, 19);
            this.textHotkey.Name = "textHotkey";
            this.textHotkey.Size = new System.Drawing.Size(154, 20);
            this.textHotkey.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textHotkey, "Press key to define as a hotkey.");
            this.textHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textHotkey_KeyDown);
            this.textHotkey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textHotkey_KeyPress);
            // 
            // chkWin
            // 
            this.chkWin.AutoSize = true;
            this.chkWin.Location = new System.Drawing.Point(63, 69);
            this.chkWin.Name = "chkWin";
            this.chkWin.Size = new System.Drawing.Size(70, 17);
            this.chkWin.TabIndex = 5;
            this.chkWin.Text = "Windows";
            this.chkWin.UseVisualStyleBackColor = true;
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
            // chkShift
            // 
            this.chkShift.AutoSize = true;
            this.chkShift.Location = new System.Drawing.Point(172, 45);
            this.chkShift.Name = "chkShift";
            this.chkShift.Size = new System.Drawing.Size(47, 17);
            this.chkShift.TabIndex = 4;
            this.chkShift.Text = "Shift";
            this.chkShift.UseVisualStyleBackColor = true;
            // 
            // chkAlt
            // 
            this.chkAlt.AutoSize = true;
            this.chkAlt.Location = new System.Drawing.Point(128, 45);
            this.chkAlt.Name = "chkAlt";
            this.chkAlt.Size = new System.Drawing.Size(38, 17);
            this.chkAlt.TabIndex = 3;
            this.chkAlt.Text = "Alt";
            this.chkAlt.UseVisualStyleBackColor = true;
            // 
            // chkControl
            // 
            this.chkControl.AutoSize = true;
            this.chkControl.Location = new System.Drawing.Point(63, 45);
            this.chkControl.Name = "chkControl";
            this.chkControl.Size = new System.Drawing.Size(59, 17);
            this.chkControl.TabIndex = 2;
            this.chkControl.Text = "Control";
            this.chkControl.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hotkey";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbSizes
            // 
            this.gbSizes.Controls.Add(this.groupBox1);
            this.gbSizes.Controls.Add(this.label6);
            this.gbSizes.Controls.Add(this.lblTextRows);
            this.gbSizes.Controls.Add(this.nudClipsLinesPerRow);
            this.gbSizes.Controls.Add(this.nudMaxClips);
            this.gbSizes.Location = new System.Drawing.Point(5, 107);
            this.gbSizes.Name = "gbSizes";
            this.gbSizes.Size = new System.Drawing.Size(224, 249);
            this.gbSizes.TabIndex = 2;
            this.gbSizes.TabStop = false;
            this.gbSizes.Text = "Clips";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.pnlHeaderColor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.pnlClipsRowColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pnlClipsBackColor);
            this.groupBox1.Controls.Add(this.pnlClipsFontColor);
            this.groupBox1.Location = new System.Drawing.Point(6, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 162);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Row Color";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlClipsRowColor
            // 
            this.pnlClipsRowColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClipsRowColor.Location = new System.Drawing.Point(13, 120);
            this.pnlClipsRowColor.Name = "pnlClipsRowColor";
            this.pnlClipsRowColor.Size = new System.Drawing.Size(26, 27);
            this.pnlClipsRowColor.TabIndex = 4;
            this.pnlClipsRowColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Background Color";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(45, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Font Color";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlClipsBackColor
            // 
            this.pnlClipsBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClipsBackColor.Location = new System.Drawing.Point(13, 87);
            this.pnlClipsBackColor.Name = "pnlClipsBackColor";
            this.pnlClipsBackColor.Size = new System.Drawing.Size(26, 27);
            this.pnlClipsBackColor.TabIndex = 1;
            this.pnlClipsBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // pnlClipsFontColor
            // 
            this.pnlClipsFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClipsFontColor.Location = new System.Drawing.Point(13, 54);
            this.pnlClipsFontColor.Name = "pnlClipsFontColor";
            this.pnlClipsFontColor.Size = new System.Drawing.Size(26, 27);
            this.pnlClipsFontColor.TabIndex = 0;
            this.pnlClipsFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Maximum Clips";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTextRows
            // 
            this.lblTextRows.AutoSize = true;
            this.lblTextRows.Location = new System.Drawing.Point(13, 22);
            this.lblTextRows.Margin = new System.Windows.Forms.Padding(0);
            this.lblTextRows.Name = "lblTextRows";
            this.lblTextRows.Size = new System.Drawing.Size(75, 13);
            this.lblTextRows.TabIndex = 15;
            this.lblTextRows.Text = "Lines per Row";
            this.lblTextRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudClipsLinesPerRow
            // 
            this.nudClipsLinesPerRow.Location = new System.Drawing.Point(115, 18);
            this.nudClipsLinesPerRow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudClipsLinesPerRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudClipsLinesPerRow.Name = "nudClipsLinesPerRow";
            this.nudClipsLinesPerRow.Size = new System.Drawing.Size(62, 20);
            this.nudClipsLinesPerRow.TabIndex = 0;
            this.toolTip1.SetToolTip(this.nudClipsLinesPerRow, "The number of rows of text to display per clip in list.");
            this.nudClipsLinesPerRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMaxClips
            // 
            this.nudMaxClips.Location = new System.Drawing.Point(115, 45);
            this.nudMaxClips.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudMaxClips.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxClips.Name = "nudMaxClips";
            this.nudMaxClips.Size = new System.Drawing.Size(62, 20);
            this.nudMaxClips.TabIndex = 3;
            this.toolTip1.SetToolTip(this.nudMaxClips, "Maximum number of clips to save.");
            this.nudMaxClips.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(154, 375);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(235, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(250, 17);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(177, 17);
            this.chkStartup.TabIndex = 4;
            this.chkStartup.Text = "Automatically start with windows";
            this.toolTip1.SetToolTip(this.chkStartup, "Check to automatically start clips.");
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // nudPreviewPopupDelay
            // 
            this.nudPreviewPopupDelay.Location = new System.Drawing.Point(115, 52);
            this.nudPreviewPopupDelay.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.nudPreviewPopupDelay.Name = "nudPreviewPopupDelay";
            this.nudPreviewPopupDelay.Size = new System.Drawing.Size(62, 20);
            this.nudPreviewPopupDelay.TabIndex = 21;
            this.toolTip1.SetToolTip(this.nudPreviewPopupDelay, "Delay in milliseconds before preview pops up.");
            // 
            // dlgColor
            // 
            this.dlgColor.AnyColor = true;
            // 
            // groupPreview
            // 
            this.groupPreview.Controls.Add(this.gbColors);
            this.groupPreview.Controls.Add(this.label4);
            this.groupPreview.Controls.Add(this.nudPreviewMaxLines);
            this.groupPreview.Controls.Add(this.label7);
            this.groupPreview.Controls.Add(this.nudPreviewPopupDelay);
            this.groupPreview.Location = new System.Drawing.Point(235, 107);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Size = new System.Drawing.Size(224, 249);
            this.groupPreview.TabIndex = 21;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Preview";
            // 
            // gbColors
            // 
            this.gbColors.Controls.Add(this.lblBackColor);
            this.gbColors.Controls.Add(this.lblFontColor);
            this.gbColors.Controls.Add(this.pnlPreviewBackColor);
            this.gbColors.Controls.Add(this.pnlPreviewFontColor);
            this.gbColors.Location = new System.Drawing.Point(6, 81);
            this.gbColors.Name = "gbColors";
            this.gbColors.Size = new System.Drawing.Size(171, 92);
            this.gbColors.TabIndex = 25;
            this.gbColors.TabStop = false;
            this.gbColors.Text = "Colors";
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(45, 59);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(92, 13);
            this.lblBackColor.TabIndex = 3;
            this.lblBackColor.Text = "Background Color";
            this.lblBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFontColor.Location = new System.Drawing.Point(45, 26);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(55, 13);
            this.lblFontColor.TabIndex = 2;
            this.lblFontColor.Text = "Font Color";
            this.lblFontColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPreviewBackColor
            // 
            this.pnlPreviewBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewBackColor.Location = new System.Drawing.Point(13, 52);
            this.pnlPreviewBackColor.Name = "pnlPreviewBackColor";
            this.pnlPreviewBackColor.Size = new System.Drawing.Size(26, 27);
            this.pnlPreviewBackColor.TabIndex = 1;
            this.pnlPreviewBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // pnlPreviewFontColor
            // 
            this.pnlPreviewFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewFontColor.Location = new System.Drawing.Point(13, 19);
            this.pnlPreviewFontColor.Name = "pnlPreviewFontColor";
            this.pnlPreviewFontColor.Size = new System.Drawing.Size(26, 27);
            this.pnlPreviewFontColor.TabIndex = 0;
            this.pnlPreviewFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Max # of Lines";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPreviewMaxLines
            // 
            this.nudPreviewMaxLines.Location = new System.Drawing.Point(115, 24);
            this.nudPreviewMaxLines.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPreviewMaxLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPreviewMaxLines.Name = "nudPreviewMaxLines";
            this.nudPreviewMaxLines.Size = new System.Drawing.Size(62, 20);
            this.nudPreviewMaxLines.TabIndex = 23;
            this.nudPreviewMaxLines.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Delay (ms)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(45, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Header Color";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeaderColor
            // 
            this.pnlHeaderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeaderColor.Location = new System.Drawing.Point(13, 21);
            this.pnlHeaderColor.Name = "pnlHeaderColor";
            this.pnlHeaderColor.Size = new System.Drawing.Size(26, 27);
            this.pnlHeaderColor.TabIndex = 6;
            this.pnlHeaderColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorControl_MouseClick);
            // 
            // formSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 421);
            this.Controls.Add(this.groupPreview);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbSizes);
            this.Controls.Add(this.gbHotkey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formSettings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clips Configuration";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formConfig_KeyDown);
            this.gbHotkey.ResumeLayout(false);
            this.gbHotkey.PerformLayout();
            this.gbSizes.ResumeLayout(false);
            this.gbSizes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClipsLinesPerRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewPopupDelay)).EndInit();
            this.groupPreview.ResumeLayout(false);
            this.groupPreview.PerformLayout();
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewMaxLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHotkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbSizes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTextRows;
        public System.Windows.Forms.CheckBox chkShift;
        public System.Windows.Forms.CheckBox chkAlt;
        public System.Windows.Forms.CheckBox chkControl;
        public System.Windows.Forms.NumericUpDown nudClipsLinesPerRow;
        public System.Windows.Forms.NumericUpDown nudMaxClips;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.CheckBox chkWin;
        public System.Windows.Forms.TextBox textHotkey;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CheckBox chkStartup;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudPreviewPopupDelay;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown nudPreviewMaxLines;
        private System.Windows.Forms.GroupBox gbColors;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblFontColor;
        public System.Windows.Forms.Panel pnlPreviewBackColor;
        public System.Windows.Forms.Panel pnlPreviewFontColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Panel pnlClipsRowColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Panel pnlClipsBackColor;
        public System.Windows.Forms.Panel pnlClipsFontColor;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Panel pnlHeaderColor;
    }
}