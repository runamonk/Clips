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
            this.label6 = new System.Windows.Forms.Label();
            this.lblTextRows = new System.Windows.Forms.Label();
            this.nudClipsLinesPerRow = new System.Windows.Forms.NumericUpDown();
            this.nudMaxClips = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.pnlBackColor = new System.Windows.Forms.Panel();
            this.pnlFontColor = new System.Windows.Forms.Panel();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPreviewPopupDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPreviewMaxLines = new System.Windows.Forms.NumericUpDown();
            this.gbHotkey.SuspendLayout();
            this.gbSizes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClipsLinesPerRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClips)).BeginInit();
            this.gbColors.SuspendLayout();
            this.groupPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewPopupDelay)).BeginInit();
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
            this.gbSizes.Controls.Add(this.label6);
            this.gbSizes.Controls.Add(this.lblTextRows);
            this.gbSizes.Controls.Add(this.nudClipsLinesPerRow);
            this.gbSizes.Controls.Add(this.nudMaxClips);
            this.gbSizes.Location = new System.Drawing.Point(234, 8);
            this.gbSizes.Name = "gbSizes";
            this.gbSizes.Size = new System.Drawing.Size(189, 93);
            this.gbSizes.TabIndex = 2;
            this.gbSizes.TabStop = false;
            this.gbSizes.Text = "Clips";
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
            this.btnOK.Location = new System.Drawing.Point(135, 237);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 237);
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
            this.chkStartup.Location = new System.Drawing.Point(21, 209);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(177, 17);
            this.chkStartup.TabIndex = 4;
            this.chkStartup.Text = "Automatically start with windows";
            this.toolTip1.SetToolTip(this.chkStartup, "Check to automatically start clips.");
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // gbColors
            // 
            this.gbColors.Controls.Add(this.lblBackColor);
            this.gbColors.Controls.Add(this.lblFontColor);
            this.gbColors.Controls.Add(this.pnlBackColor);
            this.gbColors.Controls.Add(this.pnlFontColor);
            this.gbColors.Location = new System.Drawing.Point(8, 107);
            this.gbColors.Name = "gbColors";
            this.gbColors.Size = new System.Drawing.Size(220, 91);
            this.gbColors.TabIndex = 1;
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
            // pnlBackColor
            // 
            this.pnlBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackColor.Location = new System.Drawing.Point(13, 52);
            this.pnlBackColor.Name = "pnlBackColor";
            this.pnlBackColor.Size = new System.Drawing.Size(26, 27);
            this.pnlBackColor.TabIndex = 1;
            this.pnlBackColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlBackColor_MouseClick);
            // 
            // pnlFontColor
            // 
            this.pnlFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFontColor.Location = new System.Drawing.Point(13, 19);
            this.pnlFontColor.Name = "pnlFontColor";
            this.pnlFontColor.Size = new System.Drawing.Size(26, 27);
            this.pnlFontColor.TabIndex = 0;
            this.pnlFontColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlFontColor_MouseClick);
            // 
            // dlgColor
            // 
            this.dlgColor.AnyColor = true;
            // 
            // groupPreview
            // 
            this.groupPreview.Controls.Add(this.label4);
            this.groupPreview.Controls.Add(this.nudPreviewMaxLines);
            this.groupPreview.Controls.Add(this.label7);
            this.groupPreview.Controls.Add(this.nudPreviewPopupDelay);
            this.groupPreview.Location = new System.Drawing.Point(234, 107);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Size = new System.Drawing.Size(189, 91);
            this.groupPreview.TabIndex = 21;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Preview";
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
            this.label4.Click += new System.EventHandler(this.label4_Click);
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
            this.nudPreviewMaxLines.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // formSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 282);
            this.Controls.Add(this.groupPreview);
            this.Controls.Add(this.gbColors);
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
            ((System.ComponentModel.ISupportInitialize)(this.nudClipsLinesPerRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClips)).EndInit();
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            this.groupPreview.ResumeLayout(false);
            this.groupPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreviewPopupDelay)).EndInit();
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
        private System.Windows.Forms.GroupBox gbColors;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblFontColor;
        private System.Windows.Forms.ColorDialog dlgColor;
        public System.Windows.Forms.Panel pnlBackColor;
        public System.Windows.Forms.Panel pnlFontColor;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudPreviewPopupDelay;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown nudPreviewMaxLines;
    }
}