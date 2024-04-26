using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Clips.Forms;
using Utility;

namespace Clips.Controls
{
    public enum ButtonType
    {
        Clip,
        PasswordGen,
        Menu,
        Pin,
        Seperator
    }

    public class ClipButton : Button
    {
        public delegate void ClipButtonClickedHandler(ClipButton button);

        // Segoe UI Symbol font escape codes.
        private const string IconPinned = "\uE1F6";
        private const string IconUnpinned = "\uE1F7";
        private const string IconMainmenu = "\uE0C2";
        private const string IconPassword = "\uE192";

        private const string NewXmlFile = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" PINNED_INDEX=\"{1}\" TYPE=\"{2}\">{3}\r\n</DATA>";

        private Preview _previewForm;

        public ClipButton(Config myConfig, ButtonType buttonType, string fileName, dynamic clipContents)
        {
            ButtonType = buttonType;

            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += ConfigChanged;

            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            switch (ButtonType)
            {
                case ButtonType.PasswordGen:
                {
                    Width = Height;
                    Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                    Text = IconPassword;
                    ToolTip passwordButtonTooltip = new ToolTip();
                    passwordButtonTooltip.SetToolTip(this, "Click to generate a random password and copy it to the clipboard.");
                    break;
                }
                case ButtonType.Menu:
                    Width = Height;
                    Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                    Text = IconMainmenu;
                    break;
                case ButtonType.Pin:
                {
                    Width = Height;
                    Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                    Text = IconUnpinned;
                    ToolTip pinButtonToolTip = new ToolTip();
                    pinButtonToolTip.SetToolTip(this, "Click to pin/unpin form (overrides autohide). [Press Control + P to enable/disable]");
                    break;
                }
                case ButtonType.Clip:
                    TextAlign = ContentAlignment.TopLeft;
                    ImageAlign = ContentAlignment.MiddleLeft;
                    break;
                default:
                {
                    if (IsHeaderButton)
                    {
                        Padding = new Padding(0, 0, 0, 0);
                        Margin = new Padding(0,  0, 0, 0);
                        TextAlign = ContentAlignment.MiddleCenter;
                    }

                    break;
                }
            }

            UseCompatibleTextRendering = true; // keeps text from being wrapped prematurely.
            AutoEllipsis = false;
            UseMnemonic = false;
            AutoSize = false;
            Pinned = false;
            PinnedIndex = 0;

            if (!string.IsNullOrEmpty(fileName))
                LoadFromCache(fileName);
            else if (clipContents != null)
                SaveToCache(clipContents);

            if (ButtonType == ButtonType.Clip)
            {
                KeyDown += Button_KeyDown;
                _previewForm = new Preview(ClipsConfig);
            }

            SetColors();
            CalculateSize();
        }

        private Config ClipsConfig { get; }

        public ButtonType ButtonType { get; }

        public string FileName { get; set; }

        public string FullText { get; set; }

        public bool HasImage => PreviewImageBytes != null;

        public bool Pinned { get; set; }

        public int PinnedIndex { get; set; }

        public Image PreviewImage
        {
            get
            {
                if (!HasImage) return null;

                MemoryStream ms = new MemoryStream(PreviewImageBytes);
                Image img = Image.FromStream(ms);
                ms.Dispose();
                return Funcs.ScaleImage(img, (int)(Screen.PrimaryScreen.WorkingArea.Width * .30), (int)(Screen.PrimaryScreen.WorkingArea.Height * .30));
            }
            set
            {
                PreviewImageBytes = Funcs.ImageToByteArray(value);
                Image = value.GetThumbnailImage(50, 50, null, IntPtr.Zero);
            }
        }

        public bool IsClipButton => ButtonType == ButtonType.Clip;

        private bool IsHeaderButton => IsMenuButton || IsPinButton || ButtonType == ButtonType.PasswordGen;

        public bool IsMenuButton => ButtonType == ButtonType.Menu;

        public bool IsPinButton => ButtonType == ButtonType.Pin;

        public byte[] PreviewImageBytes { get; set; }

        // hides the focus rectangle
        protected override bool ShowFocusCues => false;

        protected override void Dispose(bool disposing)
        {
            if (_previewForm != null)
            {
                _previewForm.Close();
                _previewForm = null;
            }

            base.Dispose(disposing);
        }

        // Stops the black default border from being displayed on button when the preview form is shown.
        public override void NotifyDefault(bool value) { base.NotifyDefault(false); }

        protected override void OnClick(EventArgs e)
        {
            if (ButtonType == ButtonType.Pin)
                Text = Text == IconPinned ? IconUnpinned : IconPinned;

            if (ButtonType == ButtonType.Clip)
                _previewForm.HidePreview();

            base.OnClick(e);
            if (ButtonType == ButtonType.Clip)
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
                Focus();
            }

            if (ButtonType == ButtonType.Clip)
                _previewForm.ShowPreview(this);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (ButtonType == ButtonType.Clip)
                _previewForm.HidePreview();
            BackColor = IsHeaderButton ? ClipsConfig.HeaderButtonColor : ClipsConfig.ClipsRowBackColor;
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            base.OnPaint(pea);
            if (Pinned)
            {
                // Make a little triangle the upper right corner.
                Pen pen = new Pen(ControlPaint.Dark(ClipsConfig.ClipsRowBackColor, 25), 10);
                PointF pt1 = new PointF(Left + Width - 10, -10);
                PointF pt2 = new PointF(Left + Width + 10, 10);
                pea.Graphics.DrawLine(pen, pt1, pt2);
            }
        }

        private void CalculateSize()
        {
            if (IsDisposed)
                return;

            if (ButtonType == ButtonType.Clip)
            {
                if (!HasImage && string.IsNullOrEmpty(FullText))
                    return;

                if (HasImage)
                {
                    Height = 60;
                }
                else
                {
                    Text = "";
                    Graphics g = CreateGraphics();

                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    SizeF size = g.MeasureString("x", Font, new PointF(0, 0), StringFormat.GenericTypographic);
                    int numOfCharsPerRow = (int)(ClipsConfig.FormSize.Width / size.Width);
                    int maxChars = numOfCharsPerRow * ClipsConfig.ClipsLinesPerRow;

                    if (maxChars > FullText.Length)
                        maxChars = FullText.Length;

                    Text = FullText.Substring(0, maxChars);

                    int maxRows = maxChars / numOfCharsPerRow;
                    if (maxRows == 0) maxRows = 1;
                    int fHeight = Convert.ToInt32(size.Height);

                    Height = maxRows * fHeight + 8;
                }
            }
        }

        private void LoadFromCache(string fileName)
        {
            if (!File.Exists(fileName)) return;
            FileName = fileName;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode data = doc.DocumentElement?.SelectSingleNode("/DATA");
            string type = data?.Attributes?["TYPE"]?.InnerText;

            if (doc.DocumentElement == null || data == null || type == null)
            {
                File.Delete(fileName);
                return;
            }

            Pinned = data.Attributes["PINNED"]?.InnerText == "Y";
            PinnedIndex = Convert.ToInt32(data.Attributes["PINNED_INDEX"]?.InnerText);

            if (type == "IMAGE")
            {
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(data.InnerText));
                try
                {
                    PreviewImage = Image.FromStream(ms);
                }
                finally
                {
                    ms.Close();
                }
            }
            else
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(data.InnerText);
                string decodedString = Encoding.UTF8.GetString(base64EncodedBytes);
                FullText = decodedString;
            }
        }

        public void Save()
        {
            if (File.Exists(FileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FileName);
                XmlNode data = doc.DocumentElement?.SelectSingleNode("/DATA");
                if (doc.DocumentElement == null || data == null)
                {
                    File.Delete(FileName);
                    return;
                }

                SetAttrib(doc, data, "PINNED",       Pinned ? "Y" : "N");
                SetAttrib(doc, data, "PINNED_INDEX", PinnedIndex.ToString());
                doc.Save(FileName);
            }
        }

        private void SaveToCache(dynamic clipContents)
        {
            string fileContents = "";
            string base64;
            string randFileName = Funcs.AppPath() + "\\Cache\\" + Funcs.RandomString(20, true) + ".xml"; // DateTime.Now.ToString("yyyymmddhhmmssfff")
            string strPinned = Pinned ? "Y" : "N";

            if (clipContents is string)
            {
                FullText = clipContents;
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(clipContents);
                base64 = Convert.ToBase64String(plainTextBytes);
                fileContents = string.Format(NewXmlFile, strPinned, PinnedIndex.ToString(), "TEXT", base64);
            }
            else if (clipContents is Image)
            {
                PreviewImage = clipContents;
                base64 = Convert.ToBase64String(PreviewImageBytes);
                fileContents = string.Format(NewXmlFile, strPinned, PinnedIndex.ToString(), "IMAGE", base64);
            }

            if (fileContents != "")
            {
                FileName = randFileName;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(fileContents);
                doc.Save(randFileName);
            }
        }

        private void SetAttrib(XmlDocument doc, XmlNode node, string attribName, string value)
        {
            XmlAttribute xmlAtt = node.Attributes?[attribName] == null ? doc.CreateAttribute(attribName) : node.Attributes[attribName];
            xmlAtt.Value = value;
            node.Attributes?.Append(xmlAtt);
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

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (ButtonType != ButtonType.Clip || e.KeyCode != Keys.Delete) return;
            if (Parent is ClipPanel cp)
                cp.DeleteClip(this);
            else if (Parent is ClipPinnedPanel pp)
                pp.DeleteClip(this);
        }

        private void ConfigChanged()
        {
            if (!IsDisposed)
            {
                SetColors();
                CalculateSize();
            }
        }

        public event ClipButtonClickedHandler OnClipButtonClicked;
    }
}