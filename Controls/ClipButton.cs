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

            if (OnClipButtonClicked != null)
                OnClipButtonClicked(this);
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

        protected override bool ShowFocusCues
        {
            get {
                return false;
            }
        }

    } 
}
