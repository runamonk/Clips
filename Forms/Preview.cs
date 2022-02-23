﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace Clips
{
    public partial class Preview : Form
    {
        public Preview(Config myConfig)
        {
            InitializeComponent();
            ClipsConfig = myConfig;
            AutoSize = false;
            SizeF ss = TextRenderer.MeasureText("X", PreviewText.Font);
            FTextWidth = Convert.ToInt32(ss.Width);
        }

        private readonly Config ClipsConfig;
        private readonly int FTextWidth;
        protected override CreateParams CreateParams
        {
            get {
                //Add a DropShadow
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public void ShowPreview(ClipButton clipButton)
        {
            if ((string.IsNullOrEmpty(clipButton.FullText) && (!clipButton.HasImage)) || (ClipsConfig.PreviewPopupDelay == 0))
                return;

            BackColor = ClipsConfig.PreviewBackColor;
            ForeColor = ClipsConfig.PreviewFontColor;
            PreviewText.BackColor = ClipsConfig.PreviewBackColor;
            PreviewText.ForeColor = ClipsConfig.PreviewFontColor;
            MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * .30), (int)(Screen.PrimaryScreen.WorkingArea.Height * .40));
            PreviewText.MaximumSize = MaximumSize;
            PreviewText.Dock = DockStyle.Fill;
            PreviewImage.Dock = DockStyle.Fill;

            if (!string.IsNullOrEmpty(clipButton.FullText))
            {
                PreviewText.Text = "";
                PreviewText.Visible = true;
                PreviewImage.Visible = false;

                int MaxNoOfCharsPerLine = (MaximumSize.Width / FTextWidth);
                int MaxCharsAllRows = MaxNoOfCharsPerLine * ClipsConfig.PreviewMaxLines;

                if (clipButton.FullText.Length <= MaxCharsAllRows)
                    PreviewText.Text = clipButton.FullText;
                else
                    PreviewText.Text = clipButton.FullText.Substring(0, MaxCharsAllRows);
                
                this.AutoSize = true;
            }
            else
            if (clipButton.HasImage)
            {
                this.AutoSize = false;
                PreviewImage.Image = Funcs.ScaleImage(clipButton.PreviewImage, MaximumSize.Width, MaximumSize.Height);
                PreviewText.Visible = false;
                PreviewImage.Visible = true;
                Height = PreviewImage.Image.Height;
                Width = PreviewImage.Image.Width;
            }

            // pop the form up to the left or right of the main form, try and keep
            // it on screen.
            Form MainForm = (Form)clipButton.Parent.Parent.Parent;
            int MFRight = (MainForm.Left + MainForm.Width);
            int MFLeft = (MainForm.Left - this.Width);
            int MFTop = (MainForm.Top);

            this.Top = MFTop;
            if ((MFRight + this.Width) < Screen.PrimaryScreen.WorkingArea.Width)
                this.Left = MFRight;
            else
                this.Left = MFLeft;

            TimerShowForm.Interval = ClipsConfig.PreviewPopupDelay;
            TimerShowForm.Enabled = true; 
        }

        public void HidePreview()
        {
            TimerShowForm.Enabled = false;
            PreviewText.Text = "";
            PreviewImage.Image = null;
            Hide();
        }

        private void TimerShowForm_Tick(object sender, EventArgs e)
        {
            TimerShowForm.Enabled = false;
            Funcs.ShowInactiveTopmost(this);
        }

        private void Preview_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerShowForm.Enabled = false;
        }
    }
}
