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
            Funcs.AddMenuItem(MenuRC, "Pin", MenuPin_Click);
            Funcs.AddMenuItem(MenuRC, "-", null);
            Funcs.AddMenuItem(MenuRC, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRC, "Delete", MenuDelete_Click);

            AddClipboardFormatListener(this.Handle);
            MonitorTimer = new Timer
            {
                Interval = 200,
                Enabled = false
            };

            MonitorTimer.Tick += new EventHandler(MonitorTimerTick);
            MonitorClipboard = false;
        }

        [Obsolete]
        public ClipPanel()
        {

        }

        private readonly Timer MonitorTimer;
        public bool MonitorClipboard { get; set; }

        #region Clipboard hooks
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        #endregion

        #region Events

        public delegate void ClipPinnedHandler(ClipButton Clip, bool doSave);
        public event ClipPinnedHandler OnClipPinned;

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
            if (Controls.IndexOf(b) > -1) Controls.Remove(b);
            b.OnClipButtonClicked -= ClipButtonClicked;
            OnClipPinned?.Invoke(b, true);
            InMenu = false;
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
        public void AddClipButton(ClipButton Clip, bool doSave)
        {
            Controls.Add(Clip);
            Clip.OnClipButtonClicked += new ClipButton.ClipButtonClickedHandler(ClipButtonClicked);
            Clip.ContextMenuStrip = MenuRC;
            if (doSave)
            {
                Clip.Pinned = false;
                Clip.Save();
            }
        }

        private void AddClipButton(string fileName, dynamic clipContents)
        {

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
                if (b.Pinned)
                    OnClipPinned?.Invoke(b, false);
                else
                    AddClipButton(b, false);

                if (!InLoad)
                    base.ClipAdded(b);
            }
        }

        protected override void ClipButtonClicked(ClipButton Clip)
        {
            SuspendLayout();
            base.ClipClicked(Clip);
            MonitorClipboard = false;

            if (Clip.HasImage)
            {
                MemoryStream ms = new MemoryStream(Clip.PreviewImageBytes);
                Image img = Image.FromStream(ms);
                ms.Dispose();
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
                    Clipboard.SetText(Clip.FullText);
                }
            }

            if (!Clip.Pinned)
            {
                Clip.Save();
                Controls.SetChildIndex(Clip, Controls.Count-1);
            }
            ResumeLayout();
            First();
            MonitorTimer.Enabled = true;
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
            if ((clip is Image) && (Controls.Count > 0))
            {
                if (((ClipButton)Controls[Controls.Count-1]).HasImage && Funcs.IsSame(clip, ((ClipButton)Controls[Controls.Count-1]).PreviewImageBytes))
                    return ((ClipButton)Controls[Controls.Count-1]);
            }
            else
            if ((clip is String) && (Controls.Count > 0))
            {
                if ((((ClipButton)Controls[Controls.Count - 1]).FullText != "") && (((ClipButton)Controls[Controls.Count - 1]).FullText == clip))
                    return ((ClipButton)Controls[Controls.Count - 1]);

                foreach (ClipButton b in Controls)
                {
                    if (string.IsNullOrEmpty(b.FileName))
                        continue;

                    if ((clip is String) && (b.FullText != "" && b.FullText == clip))
                        return b;
                }
            }

            return null;
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

        public void SetMonitorClipboard(bool doMonitorBoard)
        {
            MonitorClipboard = doMonitorBoard;
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
                MonitorClipboard = false;

                IDataObject obj = Clipboard.GetDataObject();
                if (obj == null)
                    return;

                if (obj.GetDataPresent(DataFormats.Text))
                {
                    if (GetClip((string)obj.GetData(DataFormats.Text)) == null)
                        AddClipButton("", ((string)obj.GetData(DataFormats.Text)).Trim());
                }           
                else
                //if (obj.GetDataPresent(DataFormats.Bitmap))
                //    AddClipButton("", (Bitmap)obj.GetData(DataFormats.Dib));  Do I want to support this?
                if (obj.GetDataPresent(DataFormats.Bitmap))
                {
                    //if (GetClip((Bitmap)obj.GetData(DataFormats.Bitmap)) == null)
                        AddClipButton("", (Bitmap)obj.GetData(DataFormats.Bitmap));
                }                   
                else
                if (obj.GetDataPresent(DataFormats.FileDrop))
                {                 
                    string s = string.Join("\n", ((string[])obj.GetData(DataFormats.FileDrop)).Select(i => i.ToString()).ToArray());
                    if (GetClip(s) == null)
                        AddClipButton("", s);
                }
                MonitorTimer.Enabled = true;
            }
            #endregion
        }
        #endregion
    }
}
