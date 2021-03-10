using System;
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
        }
        private Config ClipsConfig;

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
            if ((string.IsNullOrEmpty(clipButton.FullText) && (clipButton.FullImage == null)) || (ClipsConfig.PreviewPopupDelay == 0))
                return;

            BackColor = ClipsConfig.PreviewBackColor;
            ForeColor = ClipsConfig.PreviewFontColor;
            PreviewText.BackColor = ClipsConfig.PreviewBackColor;
            PreviewText.ForeColor = ClipsConfig.PreviewFontColor;

            if (!string.IsNullOrEmpty(clipButton.FullText))
            {
                PreviewText.Clear();
                PreviewText.Visible = true;
                PreviewImage.Visible = false;

                string[] s = clipButton.FullText.TrimStart().Split(new string[] { "\n" }, StringSplitOptions.None);
                int i = 0;
                if (s.Count() > ClipsConfig.PreviewMaxLines)
                    while (i <= ClipsConfig.PreviewMaxLines)
                    {
                        PreviewText.AppendText(s[i]);
                        i++;
                    }
                else
                    PreviewText.Text = clipButton.FullText;

                MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * .30), (int)(Screen.PrimaryScreen.WorkingArea.Height * .30));

                if (Funcs.IsUrl(PreviewText.Text))
                    PreviewText.AppendText("\n [Control + click to open.]");

                SizeF ss = TextRenderer.MeasureText(PreviewText.Text, PreviewText.Font);

                if (ss.Height+6 > MaximumSize.Height)
                    Height = MaximumSize.Height;
                else
                    Height = Convert.ToInt32(ss.Height) + 6;

                if (ss.Width + 6 > MaximumSize.Width)
                    Width = MaximumSize.Height;
                else
                    Width = Convert.ToInt32(ss.Width) + 6;
            }
            else
            if (clipButton.FullImage != null)
            {

                MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * .30), (int)(Screen.PrimaryScreen.WorkingArea.Height * .30));
                PreviewImage.Image = Funcs.ScaleImage(clipButton.FullImage, MaximumSize.Width, MaximumSize.Height);
                PreviewText.Visible = false;
                PreviewImage.Visible = true;
                Height = PreviewImage.Image.Height;
                Width = PreviewImage.Image.Width;
            }

            Form MainForm = (Form)clipButton.Parent.Parent.Parent;
            int MFRight = (MainForm.Left + MainForm.Width);
            int MFLeft = (MainForm.Left - this.Width);
            int MFTop = (MainForm.Top);

            this.Top = MFTop;
            if ((MFRight+this.Width) < Screen.PrimaryScreen.WorkingArea.Width)
                this.Left = MFRight;
            else
                this.Left = MFLeft;

            TimerShowForm.Interval = ClipsConfig.PreviewPopupDelay;
            TimerShowForm.Enabled = true; 
        }

        public void HidePreview()
        {
            TimerShowForm.Enabled = false;
            Hide();
        }

        private void TimerShowForm_Tick(object sender, EventArgs e)
        {
            TimerShowForm.Enabled = false;
            Funcs.ShowInactiveTopmost(this);
        }

        private void Preview_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
