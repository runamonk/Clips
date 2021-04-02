using Clips.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utility;

namespace Clips
{
    public enum ButtonType
    {
        Clip,
        Menu,
        Pin,
        Seperator
    }

    public partial class ClipButton : Button
    {
        private Config ClipsConfig { get; set; }

        private readonly ButtonType FButtonType;
        public ButtonType ButtonType { get { return FButtonType; } }
        public bool IsMenuButton { get { return (FButtonType == ButtonType.Menu); } }
        public bool IsPinButton { get { return (FButtonType == ButtonType.Pin); } }
        public bool IsClipButton { get { return (FButtonType == ButtonType.Clip); } }

        public string FileName { get; set; }
        public Image FullImage { get; set; }
        public string FullText { get; set; }

        public delegate void ClipButtonClickedHandler(ClipButton Button);
        public event ClipButtonClickedHandler OnClipButtonClicked;

        public ClipButton(Config myConfig, ButtonType buttonType)
        {
            FButtonType = buttonType;
            FlatAppearance.BorderSize = 0;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            if (buttonType == ButtonType.Clip)
            {
                TextAlign = ContentAlignment.TopLeft;
                ImageAlign = ContentAlignment.MiddleLeft;
            }
            else
            if (IsHeaderButton())
            {
                Padding = new Padding(0, 0, 0, 0);
                Margin = new Padding(0, 0, 0, 0);
                TextAlign = ContentAlignment.MiddleCenter;
            }

            if (IsPinButton)
            {
                ToolTip PinButtonToolTip = new ToolTip();
                PinButtonToolTip.SetToolTip(this, "Click to pin/unpin form (overrides autohide). [Press Control + P to enable/disable]");
            }

            UseCompatibleTextRendering = true; // keeps text from being wrapped prematurely.
            AutoEllipsis = false;
            UseMnemonic = false;
            AutoSize = false;       
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

        private bool IsHeaderButton()
        {
            return (IsMenuButton || IsPinButton);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            OnClipButtonClicked?.Invoke(this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (IsHeaderButton())
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
            if (IsHeaderButton())
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
