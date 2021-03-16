using Clips.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utility;

namespace Clips
{

    public partial class ClipButton : Button
    {
        private Config ClipsConfig { get; set; }
        private bool IsMenuButton = false;
        public string FileName { get; set; }
        public Image FullImage { get; set; }
        public string FullText { get; set; }

        public delegate void ClipButtonClickedHandler(ClipButton Button);
        public event ClipButtonClickedHandler OnClipButtonClicked;

        public ClipButton(Config myConfig, bool isMenuButton = false)
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            TextAlign = ContentAlignment.TopLeft;
            ImageAlign = ContentAlignment.MiddleLeft;
            UseCompatibleTextRendering = true; // keeps text from being wrapped prematurely.
            AutoEllipsis = false;
            UseMnemonic = false;
            AutoSize = false;
            IsMenuButton = isMenuButton;
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);
            SetColors();
        }

        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
        }

        // Stops the black default border from being displayed on button when the preview form is shown.
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            OnClipButtonClicked?.Invoke(this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (IsMenuButton)
            {
                BackColor = ClipsConfig.MenuSelectedColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsSelectedColor;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (IsMenuButton)
            {
                BackColor = ClipsConfig.MenuButtonColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsRowBackColor;
            }
        }


        private void SetColors()
        {
            FlatAppearance.BorderColor = BackColor;
            if (IsMenuButton)
            {
                BackColor = ClipsConfig.MenuButtonColor;
                ForeColor = ClipsConfig.MenuFontColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsRowBackColor;
                ForeColor = ClipsConfig.ClipsFontColor;
            }
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            base.OnPaint(pea);

            //if (!IsMenuButton)
            //{
            //    // Defines pen 
            //    Pen pen = new Pen(ControlPaint.Dark(ClipsConfig.ClipsRowBackColor, 25));
                               
            //    PointF pt1 = new PointF(0F, Height-1);
            //    PointF pt2 = new PointF(0F, Height);
            //    pea.Graphics.DrawLine(pen, pt1, pt2);
            //}
        }

        protected override bool ShowFocusCues
        {
            get {
                return false;
            }
        }

    } 
}
