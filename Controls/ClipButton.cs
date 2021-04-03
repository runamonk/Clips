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

        public string FileName { get; set; }

        private string FFullText;

        public string FullText
        {
            get {
                return FFullText;
            }
            set {
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

        public bool HasImage { get { return (PreviewImageBytes != null); } }

        public Image PreviewImage
        {
            get {
                if (!HasImage)
                    return null;
                else
                {
                    MemoryStream ms = new MemoryStream(PreviewImageBytes);
                    Image img = Image.FromStream(ms);
                    ms.Dispose();
                    return Funcs.ScaleImage(img, (int)(Screen.PrimaryScreen.WorkingArea.Width * .30), (int)(Screen.PrimaryScreen.WorkingArea.Height * .30));
                }
            }
            set {
                PreviewImageBytes = Funcs.ImageToByteArray(value);
                string base64 = Convert.ToBase64String(PreviewImageBytes);

                if ((FileName == "") || (!File.Exists(FileName)))
                    FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));

                // TODO DEFAULT IMAGE THUMBNAIL SIZE.             
                Image = value.GetThumbnailImage(50, 50, null, IntPtr.Zero);
                CalculateSize();
            }
        }

        public bool IsClipButton { get { return (FButtonType == ButtonType.Clip); } }

        private bool IsHeaderButton { get { return (IsMenuButton || IsPinButton); } }

        public bool IsMenuButton { get { return (FButtonType == ButtonType.Menu); } }
        
        public bool IsPinButton { get { return (FButtonType == ButtonType.Pin); } }

        public byte[] PreviewImageBytes { get; set; }

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
            if (IsHeaderButton)
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
                if (HasImage)
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

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            OnClipButtonClicked?.Invoke(this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (IsHeaderButton)
            {
                BackColor = ClipsConfig.HeaderButtonSelectedColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsSelectedColor;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (IsHeaderButton)
            {
                BackColor = ClipsConfig.HeaderButtonColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsRowBackColor;
            }
        }

        private void SetColors()
        {
            FlatAppearance.BorderColor = BackColor;
            if (IsHeaderButton)
            {
                BackColor = ClipsConfig.HeaderButtonColor;
                ForeColor = ClipsConfig.HeaderFontColor;
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
