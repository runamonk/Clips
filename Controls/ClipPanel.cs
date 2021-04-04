using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utility;

namespace Clips.Controls
{
    public partial class ClipPanel : Panel
    {
        private Config ClipsConfig { get; set; }

        #region Properties
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }
        public bool InPreview { get; set; }
        public bool MonitorClipboard { get; set; }
        #endregion

        #region Clipboard hooks
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]

        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        private const int WM_DRAWCLIPBOARD = 0x0308; 
        private readonly IntPtr _clipboardViewerNext;
        #endregion

        private readonly ClipMenu MenuRC;
        private readonly Preview PreviewForm;

        public ClipPanel(Config myConfig)
        {
            MonitorClipboard = false;
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
            _clipboardViewerNext = SetClipboardViewer(this.Handle);
            MonitorClipboard = true;
        }

        #region EventHandlers
        public delegate void ClipAddedHandler(ClipButton Clip);
        public event ClipAddedHandler OnClipAdded;

        public delegate void ClipClickedHandler(ClipButton Clip);
        public event ClipClickedHandler OnClipClicked;

        public delegate void ClipDeletedHandler();
        public event ClipDeletedHandler OnClipDeleted;

        public delegate void ClipsLoadedHandler();
        public event ClipsLoadedHandler OnClipsLoaded;
        #endregion

        private void AddClipButton(string fileName, dynamic clipContents)
        {
            if (clipContents != null)
            {
                if ((clipContents is String) && ClipExists(clipContents))
                    return;
                else 
                if ((clipContents is Image) && ClipExists(clipContents))
                    return;
            }

            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
                DeleteOldestClip();

            ClipButton b = new ClipButton(ClipsConfig, ButtonType.Clip, fileName, clipContents)
            {
                TabStop = false,
                Dock = DockStyle.Top
            };

            b.OnClipButtonClicked += new ClipButton.ClipButtonClickedHandler(ClipClicked);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);
            b.ContextMenuStrip = MenuRC;
            Controls.Add(b);

            if (!InLoad)
                OnClipAdded?.Invoke(b);
        }

        private void ClipClicked(ClipButton Clip)
        {
            void DeleteClip()
            {
                if (File.Exists(Clip.FileName))
                    File.Delete(Clip.FileName);

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
            ResumeLayout();
            First();
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

            InLoad = true;
            string[] files = Funcs.GetFiles(Funcs.AppPath() + "\\Cache", "*.xml");
            foreach (string file in files)
            {
                AddClipButton(file, null);
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

            Controls[Controls.IndexOf(b)].Dispose();

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

        #region Overrides
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

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ChangeClipboardChain(this.Handle, _clipboardViewerNext);
            base.OnHandleDestroyed(e);
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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            #region Clipboard hooks
            if ((m.Msg == WM_DRAWCLIPBOARD) && (MonitorClipboard))
            {
                IDataObject obj = Clipboard.GetDataObject();      
                if (obj.GetDataPresent(DataFormats.Text))
                    AddClipButton("", ((string)obj.GetData(DataFormats.Text)).Trim());               
                else 
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
