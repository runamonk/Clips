using Clips.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        private const string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";

        private Config ClipsConfig { get; set; }

        private readonly ButtonType FButtonType;
        public ButtonType ButtonType { get { return FButtonType; } }
        public bool IsMenuButton { get { return (FButtonType == ButtonType.Menu); } }
        public bool IsPinButton { get { return (FButtonType == ButtonType.Pin); } }
        public bool IsClipButton { get { return (FButtonType == ButtonType.Clip); } }

        public string FileName { get; set; }

        private Image FFullImage; 
        public Image FullImage
        {
            get {
                return FFullImage;
            }
            set {
                FFullImage = value;
                if (FFullImage == null)
                    return;

                string base64 = Convert.ToBase64String(Funcs.ImageToByteArray(FFullImage));

                if ((FileName == "") || (!File.Exists(FileName)))
                    FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));

                // TODO DEFAULT IMAGE THUMBNAIL SIZE.             
                Image = FFullImage.GetThumbnailImage(50, 50, null, IntPtr.Zero);
                CalculateSize();
            }
        }

        private string FFullText;
        public string FullText 
        {             
            get 
            {
                return FFullText;
            }             
            set 
            {
                FFullText = value;               
                if (FFullText == null)
                    return;
                
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(FFullText);
                string base64 = Convert.ToBase64String(plainTextBytes);
                    
                if ((FileName == "") || (!File.Exists(FileName)))
                    FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));

                CalculateSize();
            }        
        }

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

        private void CalculateSize()
        {
            if (ButtonType == ButtonType.Clip)
            {
                if (FullImage != null)
                    Height = 60;
                else
                {
                    //TODO Come up with a better way to handle displaying multiple lines per ClipButton
                    string[] s = FFullText.TrimStart().Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.None);
                    if (s.Count() >= ClipsConfig.ClipsLinesPerRow)
                        for (int i = 0; i < ClipsConfig.ClipsLinesPerRow; i++)
                        {
                            if (string.IsNullOrEmpty(Text))
                                Text = s[i] + "\n";
                            else
                                Text = Text + s[i] + "\n";
                        }
                    else
                        Text = FFullText;

                    SizeF ss = TextRenderer.MeasureText("X", Font);
                    int FHeight = Convert.ToInt32(ss.Height);
                    Height = (s.Count() > 0 && s.Count() >= ClipsConfig.ClipsLinesPerRow ? ClipsConfig.ClipsLinesPerRow * FHeight + 8 : FHeight + 8);
                }
            }
        }

        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
            CalculateSize();
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
