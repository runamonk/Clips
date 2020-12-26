using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                if (s.Count() >= maxLines)
                    while (textPreview.Lines.Count() < maxLines)
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
                    Size = MaximumSize;
                else
                    Size = image.Size;
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

        private void DoMoveFormToCursor()
        {
            Point p = new Point(Cursor.Position.X + 10, Cursor.Position.Y - 10);

            //Height
            if ((p.Y + this.Size.Width) > Screen.PrimaryScreen.WorkingArea.Height)
            {
                p.Y = (p.Y - this.Size.Height);
            }

            //Width
            if ((p.X + this.Size.Width) > Screen.PrimaryScreen.WorkingArea.Width)
            {
                p.X = (p.X - this.Size.Width);
            }

            this.Location = p;
        }

        private void timerShowForm_Tick(object sender, EventArgs e)
        {
            timerShowForm.Enabled = false;
            DoMoveFormToCursor();
            Show();
        }
    }
}
