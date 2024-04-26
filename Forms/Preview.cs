using System;
using System.Drawing;
using System.Windows.Forms;
using Clips.Controls;
using Utility;

namespace Clips.Forms
{
    public partial class Preview : Form
    {
        private readonly int _fTextWidth;

        public Preview(Config myConfig)
        {
            InitializeComponent();
            AutoSize = false;
            ClipsConfig = myConfig;
            SizeF ss = TextRenderer.MeasureText("X", PreviewText.Font);
            _fTextWidth = Convert.ToInt32(ss.Width);
        }

        private Config ClipsConfig { get; }

        protected override CreateParams CreateParams
        {
            get
            {
                //Add a DropShadow
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        protected override bool ShowWithoutActivation => true;

        public void HidePreview()
        {
            TimerShowForm.Enabled = false;
            PreviewText.Text = "";
            PreviewImage.Image = null;
            Hide();
        }


        public void ShowPreview(ClipButton clipButton)
        {
            if ((string.IsNullOrEmpty(clipButton.FullText) && !clipButton.HasImage) || ClipsConfig.PreviewPopupDelay == 0)
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

                int maxNoOfCharsPerLine = MaximumSize.Width / _fTextWidth;
                int maxCharsAllRows = maxNoOfCharsPerLine * ClipsConfig.PreviewMaxLines;

                PreviewText.Text = clipButton.FullText.Length <= maxCharsAllRows ? clipButton.FullText : clipButton.FullText.Substring(0, maxCharsAllRows);

                AutoSize = true;
            }
            else if (clipButton.HasImage)
            {
                AutoSize = false;
                PreviewImage.Image = Funcs.ScaleImage(clipButton.PreviewImage, MaximumSize.Width, MaximumSize.Height);
                PreviewText.Visible = false;
                PreviewImage.Visible = true;
                Height = PreviewImage.Image.Height;
                Width = PreviewImage.Image.Width;
            }

            // pop the form up to the left or right of the main form, try and keep
            // it on screen.
            Form mainForm = (Form)clipButton.Parent.Parent.Parent;
            int mfRight = mainForm.Left + mainForm.Width;
            int mfLeft = mainForm.Left - Width;
            int mfTop = mainForm.Top;

            Top = mfTop;
            Left = mfRight + Width < Screen.PrimaryScreen.WorkingArea.Width ? mfRight : mfLeft;

            TimerShowForm.Interval = ClipsConfig.PreviewPopupDelay;
            TimerShowForm.Enabled = true;
        }

        private void Preview_FormClosing(object sender, FormClosingEventArgs e) { TimerShowForm.Enabled = false; }

        private void TimerShowForm_Tick(object sender, EventArgs e)
        {
            TimerShowForm.Enabled = false;
            Funcs.ShowInactiveTopmost(this);
        }
    }
}