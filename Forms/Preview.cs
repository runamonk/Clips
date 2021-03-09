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
        private int FHeight = 0;
        private int FWidth = 0;

        protected override CreateParams CreateParams
        {
            get {
                //Add a DropShadow
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        public void ShowPreview(ClipButton clipButton)
        {
            if ((string.IsNullOrEmpty(clipButton.FullText) && (clipButton.FullImage == null)) || (ClipsConfig.PreviewPopupDelay == 0))
                return;

            if (!string.IsNullOrEmpty(clipButton.FullText))
            {
                BackColor = ClipsConfig.PreviewBackColor;
                ForeColor = ClipsConfig.PreviewFontColor;
                PreviewText.BackColor = BackColor;
                PreviewText.ForeColor = ForeColor;
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

                MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * .50), (int)(Screen.PrimaryScreen.WorkingArea.Height * .50));

                if (Funcs.IsUrl(PreviewText.Text))
                    PreviewText.AppendText("\n [Control + click to open.]");

                SizeF ss = TextRenderer.MeasureText(PreviewText.Text, PreviewText.Font);

                if (ss.Height+6 > MaximumSize.Height)
                    FHeight = MaximumSize.Height;
                else
                    FHeight = Convert.ToInt32(ss.Height) + 6;

                if (ss.Width + 6 > MaximumSize.Width)
                    FWidth = MaximumSize.Height;
                else
                    FWidth = Convert.ToInt32(ss.Width) + 6;
            }
            else
            if (clipButton.FullImage != null)
            {
                PreviewImage.Image = clipButton.FullImage;
                PreviewText.Visible = false;
                PreviewImage.Visible = true;

                MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width*.50), (int)(Screen.PrimaryScreen.WorkingArea.Height*.50));
                
                if (clipButton.FullImage.Height > MaximumSize.Height)
                    FHeight = MaximumSize.Height;
                else
                    FHeight = clipButton.FullImage.Height;

                if (clipButton.FullImage.Width > MaximumSize.Width)
                    FWidth = MaximumSize.Width;
                else
                    FWidth = clipButton.FullImage.Width;
            }
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
            // for some reason I couldn't just set the new form size in the ShowPreview(), it was not accurate the first time.
            if (!TimerShowForm.Enabled)
            {
                Height = FHeight;
                Width = FWidth;
                Funcs.MoveFormToCursor(this);
            }
        }
    }
}
