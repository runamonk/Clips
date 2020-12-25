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
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        public void ShowPreview(string Text, Image Img, int PopupDelay)
        {
            if ((string.IsNullOrEmpty(Text) && (Img == null)) || (PopupDelay == 0))
                return;

            if (!string.IsNullOrEmpty(Text))
            { 
                textPreview.BackColor = BackColor;
                textPreview.ForeColor = ForeColor;

                // TODO Limit the text preview to like 30 lines.
                textPreview.Text = Text;

                SizeF ss = TextRenderer.MeasureText(textPreview.Text, textPreview.Font);
                ss.Height = ss.Height + 6; 
                ss.Width = ss.Width + 6;
                Size = ss.ToSize();

                textPreview.Visible = true;
                pbPreview.Visible = false;
            }
            else
            if (Img != null)
            {
                pbPreview.Image = Img;
                textPreview.Visible = false;
                pbPreview.Visible = true;
                Size = MaximumSize;
            }
            timerShowForm.Interval = PopupDelay;
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
