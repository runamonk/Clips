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
        public Preview()
        {
            InitializeComponent();
        }

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

        public void ShowPreview(string text, Image image, int popupDelay, int maxLines)
        {
            if ((string.IsNullOrEmpty(text) && (image == null)) || (popupDelay == 0))
                return;

            if (!string.IsNullOrEmpty(text))
            {
                PreviewText.BackColor = BackColor;
                PreviewText.ForeColor = ForeColor;
                PreviewText.Clear();

                PreviewText.Visible = true;
                PreviewImage.Visible = false;

                string[] s = text.TrimStart().Split(new string[] { "\n" }, StringSplitOptions.None);
                int i = 0;
                if (s.Count() > maxLines)
                    while (i <= maxLines)
                    {
                        PreviewText.AppendText(s[i]);
                        i++;
                    }
                else
                    PreviewText.Text = text;

                if (Funcs.IsUrl(PreviewText.Text))
                    PreviewText.AppendText("\n [Control + click to open.]");

                SizeF ss = TextRenderer.MeasureText(PreviewText.Text, PreviewText.Font);
                FHeight = Convert.ToInt32(ss.Height) + 6;
                FWidth = Convert.ToInt32(ss.Width) + 6;
            }
            else
            if (image != null)
            {
                PreviewImage.Image = image;
                PreviewText.Visible = false;
                PreviewImage.Visible = true;

                MaximumSize = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width*.50), (int)(Screen.PrimaryScreen.WorkingArea.Height*.50));
                
                if (image.Height > MaximumSize.Height)
                    FHeight = MaximumSize.Height;
                else
                    FHeight = image.Height;

                if (image.Width > MaximumSize.Width)
                    FWidth = MaximumSize.Width;
                else
                    FWidth = image.Width;
            }
            TimerShowForm.Interval = popupDelay;
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
