using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                    PreviewText.Text = PreviewText.Text + "\n" + "Control + click to open.";

                SizeF ss = TextRenderer.MeasureText(PreviewText.Text, PreviewText.Font);
                ss.Height = ss.Height + 6; 
                ss.Width = ss.Width + 6;
                Size = ss.ToSize();

                PreviewText.Visible = true;
                PreviewImage.Visible = false;
            }
            else
            if (image != null)
            {
                PreviewImage.Image = image;
                PreviewText.Visible = false;
                PreviewImage.Visible = true;

                if (image.Height > MaximumSize.Height)
                    Height = MaximumSize.Height;
                else
                    Height = image.Height;

                if (image.Width > MaximumSize.Width)
                    Width = MaximumSize.Width;
                else
                    Width = image.Width;
            }
            TimerShowForm.Interval = popupDelay;
            TimerShowForm.Enabled = true; 
        }

        public void HidePreview()
        {
            TimerShowForm.Enabled = false;
            PreviewImage.Image = null;
            Hide();
        }

        private void TimerShowForm_Tick(object sender, EventArgs e)
        {
            TimerShowForm.Enabled = false;
            Funcs.MoveFormToCursor(this);
            Show();
        }
    }
}
