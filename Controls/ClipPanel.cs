using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utility;

namespace Clips.Controls
{
    public partial class ClipPanel : System.Windows.Forms.Panel
    {
        private Config ClipsConfig { get; set; }
        private bool IsHeader = false;
        private Image LastImage { get; set; }
        private string LastText { get; set; }
        public bool inMenu { get; set; }
        public bool inLoad { get; set; }
        public bool inPreview { get; set; }
 
        private ClipMenu MenuRC;
        private Preview PreviewForm = new Preview();

        private string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";

        public ClipPanel(Config myConfig, bool isHeader = false)
        {
            IsHeader = isHeader;
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);

            ToolStripMenuItem t;

            MenuRC = new ClipMenu(myConfig);
            t = new ToolStripMenuItem("&Save");
            t.Click += new EventHandler(MenuSave_Click);
            MenuRC.Items.Add(t);

            t = new ToolStripMenuItem("&Delete");
            t.Click += new EventHandler(MenuDelete_Click);
            MenuRC.Items.Add(t);

            SetColors();
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
            if (text == LastText) return;

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
        }

        public void AddItem(Image image, string fileName, bool saveToDisk = false)
        {
            if ((LastImage != null) && Funcs.IsSame(image, LastImage)) return;

            LastImage = image;

            ClipButton b = AddClipButton();
            b.Height = 60;
            b.FullImage = image;

            string base64 = Convert.ToBase64String(Funcs.ConvertImageToByteArray(b.FullImage));
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));
            else
                b.FileName = fileName;
            // TODO DEFAULT IMAGE THUMBNAIL SIZE.             
            b.Image = image.GetThumbnailImage(60, 60, null, IntPtr.Zero);

            if (OnClipAdded != null)
                OnClipAdded(b, saveToDisk);
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

                Controls.Remove(Clip);

                GC.Collect();
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

        public void DeleteOldestClip()
        {
            ClipButton cb = ((ClipButton)Controls[0]);
            if (File.Exists(cb.FileName))
                File.Delete(cb.FileName);
            Controls.RemoveAt(0);
        }

        public void LoadItems()
        {
            Controls.Clear();
            LastImage = null;
            LastText = "";
            inLoad = true;
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
                        Bitmap img = new Bitmap(ms);
                        AddItem(img, file, false);
                        //img.Dispose();
                    }
                    finally
                    {
                        ms.Close();
                        //ms.Dispose();
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
            inLoad = false;
            if (OnClipsLoaded != null)
                OnClipsLoaded();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            inMenu = true;
            ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
            if (File.Exists(b.FileName))
                File.Delete(b.FileName);

            if (Funcs.IsSame(b.FullImage, LastImage))
                LastImage = null;
            else
            if (b.FullText == LastText)
                LastText = null;

            Controls.Remove(b);
            GC.Collect();

            if (OnClipDeleted != null)
                OnClipDeleted();
            inMenu = false;
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            inMenu = true;
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
            inMenu = false;
        }

        private void PreviewHide(object sender, EventArgs e)
        {
            PreviewForm.HidePreview();
            inPreview = false;
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            inPreview = true;
            ((ClipButton)sender).Select();

            PreviewForm.BackColor = ClipsConfig.PreviewBackColor;
            PreviewForm.ForeColor = ClipsConfig.PreviewFontColor;
            PreviewForm.ShowPreview(((ClipButton)sender).FullText, ((ClipButton)sender).FullImage, ClipsConfig.PreviewPopupDelay, ClipsConfig.PreviewMaxLines);
        }

        private void SetColors()
        {
            if (IsHeader)
                BackColor = ClipsConfig.ClipsHeaderColor;
            else
                BackColor = ClipsConfig.ClipsBackColor;
        }

        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
        }
    }
}
