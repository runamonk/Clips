using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utility;
using WK.Libraries.SharpClipboardNS;

namespace Clips.Controls
{
    public partial class ClipPanel : Panel
    {
        private Config ClipsConfig { get; set; }
        private bool IsHeader = false;
        private Image LastImage { get; set; }
        private string LastText { get; set; }
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }
        public bool MonitorClipboard
        {
            get 
            {
                return clipboard.MonitorClipboard;
            }
            set 
            {
                clipboard.MonitorClipboard = value;
            }
        }
 
        private ClipMenu MenuRC;
        private Preview PreviewForm;
        private SharpClipboard clipboard;

        private string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";

        public ClipPanel(Config myConfig, bool isHeader = false)
        {            
            IsHeader = isHeader;
            ParentChanged += new EventHandler(OnParentChanged);
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);
            PreviewForm = new Preview(ClipsConfig);
            MenuRC = new ClipMenu(myConfig);
            MenuRC.ShowCheckMargin = false;
            MenuRC.ShowImageMargin = false;
            Funcs.AddMenuItem(MenuRC, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRC, "Delete", MenuDelete_Click);
            LoadItems();
            if (clipboard == null)
            {
                clipboard = new SharpClipboard();
                clipboard.MonitorClipboard = true;
                clipboard.ObservableFormats.All = true;
                clipboard.ObservableFormats.Files = true;
                clipboard.ObservableFormats.Images = true;
                clipboard.ObservableFormats.Others = true;
                clipboard.ObservableFormats.Texts = true;
                clipboard.ObserveLastEntry = false;
                clipboard.Tag = null;
                clipboard.ClipboardChanged += new EventHandler<SharpClipboard.ClipboardChangedEventArgs>(ClipboardChanged);
            }
        }
        
        #region EventHandlers
        public delegate void ClipAddedHandler(ClipButton Clip, bool ClipSavedToDisk);
        public event ClipAddedHandler OnClipAdded;

        public delegate void ClipClickedHandler(ClipButton Clip);
        public event ClipClickedHandler OnClipClicked;

        public delegate void ClipDeletedHandler();
        public event ClipDeletedHandler OnClipDeleted;
        
        public delegate void ClipsLoadedHandler();
        public event ClipsLoadedHandler OnClipsLoaded;
        #endregion

        private ClipButton AddClipButton()
        {
            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
                DeleteOldestClip();

            ClipButton b = new ClipButton(ClipsConfig)
            {
                TabStop = false,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat
            };

            b.OnClipButtonClicked += new  ClipButton.ClipButtonClickedHandler(ButtonClicked);

            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);
            b.ContextMenuStrip = MenuRC;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            b.Parent = this;
            return b;
        }

        public void AddItem(string text, string fileName, bool saveToDisk = false)
        {
            if ((text == LastText) || ClipExists(text)) return;

            SuspendLayout();

            LastText = text;
            ClipButton b = AddClipButton();

            b.AutoSize = false;
            b.AutoEllipsis = false;
            b.FullText = text;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(plainTextBytes);
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));
            else
                b.FileName = fileName;

            //TODO Come up with a better way to handle displaying multiple lines per ClipButton
            string[] s = text.TrimStart().Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.None);
            if (s.Count() >= ClipsConfig.ClipsLinesPerRow)
                for (int i = 0; i < ClipsConfig.ClipsLinesPerRow; i++)
                {
                    if (string.IsNullOrEmpty(b.Text))
                        b.Text = s[i] + "\n";
                    else
                        b.Text = b.Text + s[i] + "\n";
                }
            else
                b.Text = text;

            SizeF ss = TextRenderer.MeasureText("X", b.Font);
            int FHeight = Convert.ToInt32(ss.Height);

            b.Height = (s.Count() > 0 && s.Count() >= ClipsConfig.ClipsLinesPerRow ? ClipsConfig.ClipsLinesPerRow * FHeight + 8 : FHeight + 8);

            if (OnClipAdded != null)
                OnClipAdded(b, saveToDisk);

             ResumeLayout();
        }

        public void AddItem(Image image, string fileName, bool saveToDisk = false)
        {
            //if ((LastImage != null) && Funcs.IsSame(image, LastImage)) return;
            if ((LastImage != null) && ClipExists(image)) return;

            SuspendLayout();
            LastImage = image;
            ClipButton b = AddClipButton();
            b.Height = 60;
            b.FullImage = image;
            string base64 = Convert.ToBase64String(Funcs.ImageToByteArray(b.FullImage));
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));
            else
                b.FileName = fileName;
            // TODO DEFAULT IMAGE THUMBNAIL SIZE.             
            b.Image = image.GetThumbnailImage(60, 60, null, IntPtr.Zero);

            if (OnClipAdded != null)
                OnClipAdded(b, saveToDisk);
            ResumeLayout();
        }

        private void ButtonClicked(ClipButton Clip)
        {
            void DeleteClip()
            {
                if (File.Exists(Clip.FileName))
                    File.Delete(Clip.FileName);

                if (Funcs.IsSame(Clip.FullImage, LastImage))
                    LastImage = null;
                else
                if (Clip.FullText == LastText)
                    LastText = null;

                Controls[Controls.IndexOf(Clip)].Dispose();
                //Controls.Remove(Clip);
            }

            SuspendLayout();

            if (OnClipClicked != null)
                OnClipClicked(Clip);

            if (Clip.FullImage != null)
            {
                Image ClipToCopy = Clip.FullImage;
                DeleteClip();
                Clipboard.SetImage(ClipToCopy);
            }
            else
            if (Clip.Text != "")
            {
                if ((Form.ModifierKeys == Keys.Control) && Funcs.IsUrl(Clip.FullText))
                {
                    System.Diagnostics.Process.Start(Clip.FullText);
                }
                else
                {
                   string TextToCopy = Clip.FullText;
                   DeleteClip();
                   Clipboard.SetText(TextToCopy);
                }
            }

            ResumeLayout();
        }

        public void CleanupCache()
        {
            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
            {
                int clipsToDelete = (Controls.Count - ClipsConfig.ClipsMaxClips);
                while (clipsToDelete > 0)
                {
                    DeleteOldestClip();
                    clipsToDelete--;
                }
            }
        }
               
        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                AddItem(clipboard.ClipboardText.Trim(), null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                AddItem(clipboard.ClipboardImage, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join("\n", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                AddItem(s, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
                AddItem(clipboard.ClipboardObject.ToString(), null, true);
            }
        }

        private bool ClipExists(string text)
        {
            foreach (ClipButton b in Controls)
            {
                if (b.FullText != "" && b.FullText == text)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ClipExists(Image image)
        {
            foreach (ClipButton b in Controls)
            {
                if (b.FullImage != null && Funcs.IsSame(b.FullImage, image))
                {
                    return true;
                }
            }

            return false;
        }

        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
            CleanupCache();
            LoadItems();         
        }

        public void DeleteOldestClip()
        {
            ClipButton cb = ((ClipButton)Controls[0]);
            if (File.Exists(cb.FileName))
                File.Delete(cb.FileName);
            Controls[0].Dispose();
            //Controls.RemoveAt(0);
        }

        public void LoadItems()
        {
            SuspendLayout();
            Controls.Clear();
            LastImage = null;
            LastText = "";
            InLoad = true;
            string[] files = Funcs.GetFiles(Funcs.AppPath() + "\\Cache", "*.xml");
            foreach (string file in files)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNode data = doc.DocumentElement.SelectSingleNode("/DATA");
                string type = data.Attributes["TYPE"]?.InnerText;

                if (type == "IMAGE")
                {
                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(data.InnerText));
                    try
                    {
                        Image img = Image.FromStream(ms);
                        AddItem(img, file, false);
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
                    AddItem(decodedString, file, false);
                }
                doc = null;
            }
            InLoad = false;
            if (OnClipsLoaded != null)
                OnClipsLoaded();
            ResumeLayout();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
            if (File.Exists(b.FileName))
                File.Delete(b.FileName);

            if (Funcs.IsSame(b.FullImage, LastImage))
                LastImage = null;
            else
            if (b.FullText == LastText)
                LastText = null;

            Controls[Controls.IndexOf(b)].Dispose();
            //Controls.Remove(b);

            if (OnClipDeleted != null)
                OnClipDeleted();
            InMenu = false;
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            InMenu = true;
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = "c:\\"
            };

            if (((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).Text != "")
            {
                dlg.Filter = "Text (*.txt)|Any (*.*)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    System.IO.File.WriteAllText(dlg.FileName, ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).Text);
            }
            else
                if (((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).FullImage != null)
            {
                dlg.Filter = "Picture (*.png)|Jpeg (*.jpg)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).FullImage.Save(dlg.FileName);
            }
            InMenu = false;
        }

        private void OnParentChanged(object sender, EventArgs e)
        {
            if (!IsHeader)
                Parent.VisibleChanged += new EventHandler(OnVisibleChanged);
        }

        private void PreviewHide(object sender, EventArgs e)
        {
            PreviewForm.HidePreview();
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            PreviewForm.ShowPreview(((ClipButton)sender));
        }

        private void SetColors()
        {
            if (IsHeader)
                BackColor = ClipsConfig.ClipsHeaderColor;
            else
                BackColor = ClipsConfig.ClipsBackColor;
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            PreviewHide(null, null);
        }
    }
}
