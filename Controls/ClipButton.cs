using Clips.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utility;
using static Clips.Controls.BasePanel;

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
        public ClipButton(Config myConfig, ButtonType buttonType, string fileName, dynamic clipContents)
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
            Pinned = false;
            PinnedIndex = 0;
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new ConfigChangedHandler(ConfigChanged);
            
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

        private bool IsHeaderButton { get { return (IsMenuButton || IsPinButton); } }

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
            if (ButtonType == ButtonType.Clip)
            {
                if ((!HasImage) && ((FullText == null) || (FullText == "")))
                    return;

                if (HasImage)
                    Height = 60;
                else
                {
                    Text = "";
                    //TODO Come up with a better way to handle displaying multiple lines per ClipButton
                    string[] s = FullText.TrimStart().Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.None);
                    if (s.Count() >= ClipsConfig.ClipsLinesPerRow)
                        for (int i = 0; i < ClipsConfig.ClipsLinesPerRow; i++)
                        {
                            if (string.IsNullOrEmpty(Text))
                                Text = s[i] + "\n";
                            else
                                Text = Text + s[i] + "\n";
                        }
                    else
                        Text = FullText;

                    SizeF ss = TextRenderer.MeasureText("X", Font);
                    int FHeight = Convert.ToInt32(ss.Height);
                    Height = (s.Count() > 0 && s.Count() >= ClipsConfig.ClipsLinesPerRow ? ClipsConfig.ClipsLinesPerRow * FHeight + 8 : FHeight + 8);
                }
            }
        }

        private void ConfigChanged()
        {
            SetColors();
            CalculateSize();
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
            string randFileName = (Funcs.AppPath() + "\\Cache\\" + DateTime.Now.ToString("yyyymmddhhmmssfff") + Funcs.RandomString(10, true) + ".xml");
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

        protected override void OnClick(EventArgs e)
        {
            PreviewForm.HidePreview();
            base.OnClick(e);
            OnClipButtonClicked?.Invoke(this);
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

            //if (!IsMenuButton)
            //{
            //    // Defines pen 
            //    Pen pen = new Pen(ControlPaint.Dark(ClipsConfig.ClipsRowBackColor, 25));
                               
            //    PointF pt1 = new PointF(0F, Height-1);
            //    PointF pt2 = new PointF(0F, Height);
            //    pea.Graphics.DrawLine(pen, pt1, pt2);
            //}
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
