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
    public partial class formPreview : Form
    {
        public formPreview()
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
                textPreview.BackColor = BackColor;
                textPreview.ForeColor = ForeColor;
                textPreview.Clear();

                string[] s = text.TrimStart().Split(new string[] { "\n" }, StringSplitOptions.None);
                int i = 0;
                if (s.Count() > maxLines)
                    while (i <= maxLines)
                    {
                        textPreview.AppendText(s[i]);
                        i++;
                    }
                else
                    textPreview.Text = text;

                SizeF ss = TextRenderer.MeasureText(textPreview.Text, textPreview.Font);
                ss.Height = ss.Height + 6; 
                ss.Width = ss.Width + 6;
                Size = ss.ToSize();

                textPreview.Visible = true;
                pbPreview.Visible = false;
            }
            else
            if (image != null)
            {
                pbPreview.Image = image;
                textPreview.Visible = false;
                pbPreview.Visible = true;

                if (image.Height > MaximumSize.Height)
                    Height = MaximumSize.Height;
                else
                    Height = image.Height;

                if (image.Width > MaximumSize.Width)
                    Width = MaximumSize.Width;
                else
                    Width = image.Width;
            }
            timerShowForm.Interval = popupDelay;
            timerShowForm.Enabled = true;
        }

        public void HidePreview()
        {
            timerShowForm.Enabled = false;
            pbPreview.Image = null;
            Hide();
        }

        private void TimerShowForm_Tick(object sender, EventArgs e)
        {
            timerShowForm.Enabled = false;
            Funcs.MoveFormToCursor(this);
            Show();
        }
    }
}
