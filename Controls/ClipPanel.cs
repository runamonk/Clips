using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private Image LastImage { get; set; }
        private string LastText { get; set; }
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }
        public bool InPreview { get; set; }

        public bool MonitorClipboard
        {
            get {
                return clipboard.MonitorClipboard;
            }
            set {
                clipboard.MonitorClipboard = value;
            }
        }

        private readonly ClipMenu MenuRC;
        private readonly Preview PreviewForm;
        private readonly SharpClipboard clipboard;
        
        protected override CreateParams CreateParams
        {
            // Force the scrollbar to always be in position. That way we can just hide it all the time without
            // having to try and account for it during the autosize or resize.
            get {
                var cp = base.CreateParams;
                cp.Style |= 0x00200000; // WS_VSCROLL
                return cp;
            }
        }

        public ClipPanel(Config myConfig)
        {
            VerticalScroll.Enabled = true;
            HorizontalScroll.Enabled = false;
            AutoScroll = true;
           
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);
            PreviewForm = new Preview(ClipsConfig);
            DoubleBuffered = true;
            MenuRC = new ClipMenu(myConfig)
            {
                ShowCheckMargin = false,
                ShowImageMargin = false
            };
            Funcs.AddMenuItem(MenuRC, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRC, "Delete", MenuDelete_Click);
            LoadItems();
            if (clipboard == null)
            {
                clipboard = new SharpClipboard
                {
                    MonitorClipboard = true
                };
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

        protected override void OnParentChanged(EventArgs e)
        {
            // Preview form the first time it was shown was some weird default size I could not
            // get around. ,,|,, M$ - I'll show it off screen first and then hide it.
            PreviewForm.Left = -8000;
            PreviewForm.Top = 0;
            PreviewForm.Show();
            PreviewForm.Hide();
            base.OnParentChanged(e);
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

            ClipButton b = new ClipButton(ClipsConfig, ButtonType.Clip)
            {
                TabStop = false,
                Dock = DockStyle.Top
            };

            b.OnClipButtonClicked += new ClipButton.ClipButtonClickedHandler(ButtonClicked);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);
            b.ContextMenuStrip = MenuRC;
            Controls.Add(b);
            return b;
        }

        public void AddItem(string text, string fileName, bool saveToDisk = false)
        {
            if (string.IsNullOrEmpty(text) || (text == LastText) || ClipExists(text)) return;
            SuspendLayout();
            LastText = text;
            ClipButton b = AddClipButton();
            b.AutoSize = false;
            b.AutoEllipsis = false;
            b.FileName = fileName;
            b.FullText = text;       
            OnClipAdded?.Invoke(b, saveToDisk);
            ResumeLayout();
        }

        public void AddItem(Image image, string fileName, bool saveToDisk = false)
        {
            if ((LastImage != null) && ClipExists(image)) return;
            SuspendLayout();
            LastImage = image;
            ClipButton b = AddClipButton();
            b.FileName = fileName;
            b.PreviewImage = image;
            OnClipAdded?.Invoke(b, saveToDisk);
            ResumeLayout();
        }

        private void ButtonClicked(ClipButton Clip)
        {
            void DeleteClip()
            {
                if (File.Exists(Clip.FileName))
                    File.Delete(Clip.FileName);

                if (Funcs.IsSame(LastImage, Clip.PreviewImageBytes))
                    LastImage = null;
                else
                if (Clip.FullText == LastText)
                    LastText = null;

                Controls[Controls.IndexOf(Clip)].Dispose();
            }

            SuspendLayout();
            PreviewHide(null, null);
            OnClipClicked?.Invoke(Clip);

            if (Clip.HasImage)
            {
                MemoryStream ms = new MemoryStream(Clip.PreviewImageBytes);
                Image img = Image.FromStream(ms);
                ms.Dispose();
                DeleteClip();
                Clipboard.SetImage(img);
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
            Controls[Controls.Count - 1].Select();
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
                if (b.HasImage && Funcs.IsSame(image, b.PreviewImageBytes))
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
        }

        public void DeleteOldestClip()
        {
            ClipButton cb = ((ClipButton)Controls[0]);
            if (File.Exists(cb.FileName))
                File.Delete(cb.FileName);
            Controls[0].Dispose();
        }

        public void First()
        {
            if (Controls.Count > 0)
            {
                ScrollControlIntoView(Controls[Controls.Count - 1]);
                Controls[Controls.Count - 1].Select();
            }          
        }

        public void Last()
        {
            if (Controls.Count > 0)
            {
                ScrollControlIntoView(Controls[0]);
                Controls[0].Select();
            }
        }

        public void LoadItems()
        {
            SuspendLayout();
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
            ResumeLayout();
            OnClipsLoaded?.Invoke();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
            if (File.Exists(b.FileName))
                File.Delete(b.FileName);

            if (Funcs.IsSame(LastImage, b.PreviewImageBytes))
                LastImage = null;
            else
            if (b.FullText == LastText)
                LastText = null;

            Controls[Controls.IndexOf(b)].Dispose();
            //Controls.Remove(b);

            OnClipDeleted?.Invoke();
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
                if (((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).HasImage)
            {
                dlg.Filter = "Picture (*.png)|Jpeg (*.jpg)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).PreviewImage.Save(dlg.FileName);
            }
            InMenu = false;
        }

        private void PreviewHide(object sender, EventArgs e)
        {
            PreviewForm.HidePreview();
            InPreview = false;
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            InPreview = true;
            PreviewForm.ShowPreview(((ClipButton)sender));
        }

        private void SetColors()
        {
                BackColor = ClipsConfig.ClipsBackColor;
        }
    }
}
