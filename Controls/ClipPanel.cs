using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Utility;

namespace Clips.Controls
{
    public partial class ClipPanel : BasePanel
    {
        public ClipPanel(Config myConfig) : base(myConfig)
        {
            MonitorClipboard = false;

            MenuRC.Opening += MenuRC_Opening;
            PinMenuItem = Funcs.AddMenuItem(MenuRC, "Pin", MenuPin_Click);
            Funcs.AddMenuItem(MenuRC, "-", null);      
            Funcs.AddMenuItem(MenuRC, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRC, "Delete", MenuDelete_Click);

            LoadItems();
            AddClipboardFormatListener(this.Handle);
            MonitorTimer = new Timer
            {
                Interval = 200,
                Enabled = false
            };
            
            MonitorTimer.Tick += new EventHandler(MonitorTimerTick);
            MonitorClipboard = true;
        }

        #region Properties
        public bool MonitorClipboard { get; set; }
        #endregion

        #region Privates          
        private readonly Timer MonitorTimer;
        private ToolStripMenuItem PinMenuItem;
        #endregion

        #region Clipboard hooks
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        #endregion

        #region Events

        protected override void ConfigChanged()
        {
            base.ConfigChanged();
            CleanupCache();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            DeleteClip(((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl));
            base.ClipDeleted();
            InMenu = false;
        }

        private void MenuPin_Click(object sender, EventArgs e)
        {
            InMenu = true;
            ClipButton b = ((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl);
            ClipPinned(b, true);
            InMenu = false;
        }

        private void MenuRC_Opening(object sender, EventArgs e)
        {
            if (((ClipButton)(((ClipMenu)sender).SourceControl)).Pinned)
                PinMenuItem.Text = "Unpin";
            else
                PinMenuItem.Text = "Pin";
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            InMenu = true;
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = "c:\\"
            };
            ClipButton b = ((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl);

            if (b.Text != "")
            {
                dlg.Filter = "Text (*.txt)|Any (*.*)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                    System.IO.File.WriteAllText(dlg.FileName, b.Text);
            }
            else
            if (b.HasImage)
            {
                dlg.Filter = "Picture (*.png)|Jpeg (*.jpg)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                    b.PreviewImage.Save(dlg.FileName);
            }
            InMenu = false;
        }

        private void MonitorTimerTick(object sender, EventArgs e)
        {
            MonitorTimer.Enabled = false;
            MonitorClipboard = true;
        }

        #endregion

        #region Methods
        private void AddClipButton(string fileName, dynamic clipContents)
        {
            if (!InLoad && (clipContents != null))
            {
                // Windows/Applications will put multiple copies of entries into the clipboard in multiple formats.
                // I don't care about the additionals, I will convert it to the format I want and store it.
                // so when a new clip is added I disable monitoring for 200ms-ish. This way I don't have to 
                // store off the "last" clip or check for a duplicates multiple times. This is much less intensive.
                // Why couldn't they store off a reference id or something so we know they are all related? lazy.
                MonitorClipboard = false;
                MonitorTimer.Enabled = true;

                ClipButton find = GetClip(clipContents);
                if (find != null)
                    DeleteClip(find);
            }

            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
                DeleteOldestClip();

            ClipButton b = new ClipButton(ClipsConfig, ButtonType.Clip, fileName, clipContents)
            {
                TabStop = false,
                Dock = DockStyle.Top
            };

            if ((b != null) && (b.ButtonType == ButtonType.Clip) && string.IsNullOrEmpty(b.FullText) && !b.HasImage)
            {
                DeleteClip(b);
            }               
            else
            {
                b.OnClipButtonClicked += new ClipButton.ClipButtonClickedHandler(ClipButtonClicked);
                b.ContextMenuStrip = MenuRC;

                Controls.Add(b);

                if (!InLoad)
                    base.ClipAdded(b);
            }
        }

        private void ClipButtonClicked(ClipButton Clip)
        {
            SuspendLayout();
            base.ClipClicked(Clip);

            if (Clip.HasImage)
            {              
                MemoryStream ms = new MemoryStream(Clip.PreviewImageBytes);
                Image img = Image.FromStream(ms);
                ms.Dispose();
                if (!Clip.Pinned)
                    DeleteClip(Clip);
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
                    if (!Clip.Pinned)
                        DeleteClip(Clip);
                    Clipboard.SetText(Clip.FullText);
                }
            }
            ResumeLayout();
            First();
        }

        private void ClipPinned(ClipButton b, bool DoSave)
        {
            if (DoSave)
            {
                if (b.Pinned)
                {
                    b.Pinned = false;
                    b.PinnedIndex = 0;
                }                 
                else
                {
                    b.Pinned = true;
                    b.PinnedIndex = GetPinnedIndex();
                }                    

                b.Save();
            }
            Controls.SetChildIndex(b, Controls.Count-1);
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

        private void DeleteClip(ClipButton Clip)
        {
            if (string.IsNullOrEmpty(Clip.FileName))
                return;

            if (File.Exists(Clip.FileName))
                File.Delete(Clip.FileName);

            if (Controls.IndexOf(Clip) > 0)
            {
                Clip.OnClipButtonClicked -= ClipButtonClicked;
                Controls[Controls.IndexOf(Clip)].Dispose();
            }               
        }

        public void DeleteOldestClip()
        {
            if (!((ClipButton)Controls[0]).Pinned)
                DeleteClip(((ClipButton)Controls[0]));
        }

        public void First()
        {
            if (Controls.Count > 0)
            {
                ScrollControlIntoView(Controls[Controls.Count - 1]);
                Controls[Controls.Count - 1].Select();
            }          
        }

        private ClipButton GetClip(dynamic clip)
        {
            if (clip is Image) return null; // don't look for duplicate images.

            foreach (ClipButton b in Controls)
            {
                if (string.IsNullOrEmpty(b.FileName))
                    continue;

                if ((clip is Image) && (b.HasImage && Funcs.IsSame(clip, b.PreviewImageBytes)))
                    return b;
                else
                if ((clip is String) && (b.FullText != "" && b.FullText == clip))
                    return b;
            }
            return null;
        }

        private int GetPinnedIndex()
        {
            return 0;
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
            InLoad = true;
            string[] files = Funcs.GetFiles(Funcs.AppPath() + "\\Cache", "*.xml");
            foreach (string file in files)
            {
                AddClipButton(file, null);
            }
            InLoad = false;
            ResumeLayout();
            base.ClipsLoaded();
        }

        #endregion

        #region Overrides & Clipboard hooks
        protected override void OnHandleDestroyed(EventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // Why does pasting an image into an outlook email copy the image to the clipboard??
            #region Clipboard hooks
            if ((m.Msg == WM_CLIPBOARDUPDATE) && (MonitorClipboard))
            {
                IDataObject obj = Clipboard.GetDataObject();
                if (obj == null)
                    return;

                if (obj.GetDataPresent(DataFormats.Text))
                    AddClipButton("", ((string)obj.GetData(DataFormats.Text)).Trim());
                else
                //if (obj.GetDataPresent(DataFormats.Bitmap))
                //    AddClipButton("", (Bitmap)obj.GetData(DataFormats.Dib));  Do I want to support this?
                if (obj.GetDataPresent(DataFormats.Bitmap))
                    AddClipButton("", (Bitmap)obj.GetData(DataFormats.Bitmap));
                else
                if (obj.GetDataPresent(DataFormats.FileDrop))
                {
                    string s = string.Join("\n", ((string[])obj.GetData(DataFormats.FileDrop)).Select(i => i.ToString()).ToArray());
                    AddClipButton("", s);
                }
            }
            #endregion
        }
        #endregion

    }
}
