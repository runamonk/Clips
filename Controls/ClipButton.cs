using Clips.Controls;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utility;
using static Clips.Controls.BasePanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Clips
{
    public enum ButtonType
    {
        Clip,
        PasswordGen,
        Menu,
        Pin,
        Seperator
    }

    public partial class ClipButton : Button
    {
        private const string ICON_PINNED = "\uE1F6";
        private const string ICON_UNPINNED = "\uE1F7";
        private const string ICON_MAINMENU = "\uE0C2";
        private const string ICON_PASSWORD = "\uE192";

        public ClipButton(Config myConfig, ButtonType buttonType, string fileName, dynamic clipContents)
        {
            FButtonType = buttonType;
            FlatAppearance.BorderSize = 0;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            if (buttonType== ButtonType.PasswordGen) 
            {
                Width = Height;
                Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                Text = (ICON_PASSWORD);
                ToolTip PasswordButtonTooltip = new ToolTip();
                PasswordButtonTooltip.SetToolTip(this, "Click to generate a random password and copy it to the clipboard.");
            }
            else
            if (buttonType== ButtonType.Menu) 
            {
                Width = Height;
                Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                Text = (ICON_MAINMENU);
            }
            else
            if (buttonType== ButtonType.Pin)
            {
                Width = Height;
                Font = new Font("Segoe UI Symbol", 8, FontStyle.Regular);
                Text = (ICON_UNPINNED);
                ToolTip PinButtonToolTip = new ToolTip();
                PinButtonToolTip.SetToolTip(this, "Click to pin/unpin form (overrides autohide). [Press Control + P to enable/disable]");
            }
            
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

            UseCompatibleTextRendering = true; // keeps text from being wrapped prematurely.
            AutoEllipsis = false;
            UseMnemonic = false;
            AutoSize = false;
            Pinned = false;
            PinnedIndex = 0;
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new ConfigChangedHandler(ConfigChanged);

            if (buttonType == ButtonType.Clip)
                KeyDown += new KeyEventHandler(Button_KeyDown);  
                                    
            PreviewForm = new Preview(ClipsConfig);

            SetColors();
            if (!string.IsNullOrEmpty(fileName))
                LoadFromCache(fileName);
            else
            if (clipContents != null)
                SaveToCache(clipContents);
            CalculateSize();
        }

        #region Privates
        private const string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" PINNEDINDEX=\"{1}\" TYPE=\"{2}\">{3}\r\n</DATA>";
        #endregion

        #region Properties
        private Config ClipsConfig { get; set; }

        private readonly ButtonType FButtonType;
        public ButtonType ButtonType { get { return FButtonType; } }

        public string FileName { get; set; }

        public string FullText { get; set; }

        public bool HasImage { get { return (PreviewImageBytes != null); } }

        public bool Pinned { get; set; } 

        public int PinnedIndex { get; set; }

        private Preview PreviewForm;

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
                Image = value.GetThumbnailImage(50, 50, null, IntPtr.Zero);
            }
        }

        public bool IsClipButton { get { return (FButtonType == ButtonType.Clip); } }

        private bool IsHeaderButton { get { return (IsMenuButton || IsPinButton || FButtonType == ButtonType.PasswordGen); } }

        public bool IsMenuButton { get { return (FButtonType == ButtonType.Menu); } }
        
        public bool IsPinButton { get { return (FButtonType == ButtonType.Pin); } }

        public byte[] PreviewImageBytes { get; set; }

        #endregion

        #region Events
        public delegate void ClipButtonClickedHandler(ClipButton Button);
        public event ClipButtonClickedHandler OnClipButtonClicked;
        #endregion

        #region Methods
        private void CalculateSize()
        {
            if (IsDisposed)
                return;

            if (ButtonType == ButtonType.Clip)
            {
                if ((!HasImage) && ((FullText == null) || (FullText == "")))
                    return;

                if (HasImage)
                    Height = 60;
                else
                {
                    Text = "";
                    Graphics g = this.CreateGraphics();
                    
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    SizeF size = g.MeasureString("x", this.Font, new PointF(0, 0), StringFormat.GenericTypographic);
                    int NumOfCharsPerRow = (int)(ClipsConfig.FormSize.Width / size.Width);
                    int maxChars = (NumOfCharsPerRow * ClipsConfig.ClipsLinesPerRow);

                    if (maxChars > FullText.Length)
                        maxChars = FullText.Length;

                    Text = FullText.Substring(0, maxChars);

                    int maxRows = (maxChars / NumOfCharsPerRow);
                    if (maxRows == 0) maxRows = 1;
                    int FHeight = Convert.ToInt32(size.Height);

                    Height = (maxRows * FHeight + 8);
                }
            }
        }

        private void ConfigChanged()
        {
            if (! IsDisposed)
            {
                SetColors();
                CalculateSize();
            }
        }

        private void LoadFromCache(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileName = fileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode data = doc.DocumentElement.SelectSingleNode("/DATA");
                string type = data.Attributes["TYPE"]?.InnerText;
                Pinned = (data.Attributes["PINNED"]?.InnerText == "Y");
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
        }

        public void Save()
        {
            if (File.Exists(FileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FileName);
                XmlNode data = doc.DocumentElement.SelectSingleNode("/DATA");
                SetAttrib(doc, data, "PINNED", (Pinned == true ? "Y" : "N"));
                SetAttrib(doc, data, "PINNED_INDEX", PinnedIndex.ToString());
                doc.Save(FileName);
            }
        }

        private void SaveToCache(dynamic clipContents)
        {
            string fileContents = "";
            string base64;
            string randFileName = (Funcs.AppPath() + "\\Cache\\" + Funcs.RandomString(20, true) + ".xml"); // DateTime.Now.ToString("yyyymmddhhmmssfff")
            string strPinned = (Pinned == true ? "Y" : "N");
 
            if (clipContents is string)
            {
                FullText = clipContents;
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(clipContents);
                base64 = Convert.ToBase64String(plainTextBytes);
                fileContents = string.Format(new_xml_file, strPinned, PinnedIndex.ToString(), "TEXT", base64);
            }
            else
            if (clipContents is Image)
            {
                PreviewImage = clipContents;
                base64 = Convert.ToBase64String(PreviewImageBytes);
                fileContents = string.Format(new_xml_file, strPinned, PinnedIndex.ToString(), "IMAGE", base64);
            }

            if (fileContents != "")
            {
                FileName = randFileName;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(fileContents);
                doc.Save(randFileName);
            }
        }

        private void SetAttrib(XmlDocument doc, XmlNode Node, string AttribName, string Value)
        {
            XmlAttribute XmlAtt;
            if (Node.Attributes[AttribName] == null)
            {
                XmlAtt = doc.CreateAttribute(AttribName);
            }
            else
                XmlAtt = Node.Attributes[AttribName];

            XmlAtt.Value = Value;
            Node.Attributes.Append(XmlAtt);
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
        #endregion

        #region Overrides
        // Stops the black default border from being displayed on button when the preview form is shown.
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ButtonType == ButtonType.Clip) && (e.KeyCode == Keys.Delete))
            {
                ((ClipPanel)Parent).DeleteClip(this);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (ButtonType== ButtonType.Pin) 
            {
                if (Text == ICON_PINNED)
                    Text = ICON_UNPINNED;
                else
                    Text = ICON_PINNED;
            }
            PreviewForm.HidePreview();
            base.OnClick(e);
            OnClipButtonClicked?.Invoke(this);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            PreviewForm.Close();
            PreviewForm = null;
            base.OnHandleDestroyed(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            PreviewForm.ShowPreview(this);
            if (IsHeaderButton)
            {
                BackColor = ClipsConfig.HeaderButtonSelectedColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsSelectedColor;
                Focus();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            PreviewForm.HidePreview();
            if (IsHeaderButton)
            {
                BackColor = ClipsConfig.HeaderButtonColor;
            }
            else
            {
                BackColor = ClipsConfig.ClipsRowBackColor;
            }
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

        // hides the focus rectangle
        protected override bool ShowFocusCues
        {
            get {
                return false;
            }
        }                                                                                                                                                                                                                    

        #endregion
    } 
}
