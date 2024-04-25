using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Utility;

namespace Clips.Controls
{
    public class ClipPanel : BasePanel
    {
        private readonly Timer _monitorTimer;

        public ClipPanel(Config myConfig) : base(myConfig)
        {
            Funcs.AddMenuItem(MenuRc, "Pin", MenuPin_Click);
            Funcs.AddMenuItem(MenuRc, "-", null);
            Funcs.AddMenuItem(MenuRc, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRc, "Delete", MenuDelete_Click);

            AddClipboardFormatListener(Handle);
            _monitorTimer = new Timer
            {
                Interval = 200,
                Enabled = false
            };

            _monitorTimer.Tick += MonitorTimerTick;
            MonitorClipboard = false;
        }

        [Obsolete]
        public ClipPanel()
        {
        }

        public bool MonitorClipboard { get; set; }

        #region Clipboard hooks

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        private const int WmClipboardupdate = 0x031D;

        #endregion

        #region Events

        public delegate void ClipPinnedHandler(ClipButton clip, bool doSave);

        public event ClipPinnedHandler OnClipPinned;

        protected override void ConfigChanged()
        {
            base.ConfigChanged();
            CleanupCache();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            DeleteClip((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl);
            InMenu = false;
        }

        private void MenuPin_Click(object sender, EventArgs e)
        {
            InMenu = true;
            var b = (ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (Controls.IndexOf(b) > -1) Controls.Remove(b);
            b.OnClipButtonClicked -= ClipButtonClicked;
            OnClipPinned?.Invoke(b, true);
            InMenu = false;
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            InMenu = true;
            var dlg = new SaveFileDialog
            {
                InitialDirectory = "c:\\"
            };
            var b = (ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl;

            if (b.Text != "")
            {
                dlg.Filter = "Text (*.txt)|Any (*.*)";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(dlg.FileName, b.Text);
            }
            else if (b.HasImage)
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
            _monitorTimer.Enabled = false;
            MonitorClipboard = true;
        }

        #endregion

        #region Methods

        public void AddClipButton(ClipButton clip, bool doSave)
        {
            Controls.Add(clip);
            clip.OnClipButtonClicked += ClipButtonClicked;
            clip.ContextMenuStrip = MenuRc;
            if (doSave)
            {
                clip.Pinned = false;
                clip.Save();
            }
        }

        private void AddClipButton(string fileName, dynamic clipContents)
        {
            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
                DeleteOldestClip();

            var b = new ClipButton(ClipsConfig, ButtonType.Clip, fileName, clipContents)
            {
                TabStop = false,
                Dock = DockStyle.Top
            };

            if (b.ButtonType == ButtonType.Clip && string.IsNullOrEmpty(b.FullText) && !b.HasImage)
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
                    ClipAdded(b);
            }
        }

        protected override void ClipButtonClicked(ClipButton clip)
        {
            SuspendLayout();
            ClipClicked(clip);
            MonitorClipboard = false;

            if (clip.HasImage)
            {
                var ms = new MemoryStream(clip.PreviewImageBytes);
                var img = Image.FromStream(ms);
                ms.Dispose();
                try
                {
                    Clipboard.SetImage(img);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Clip appears to be locked.", e.Message, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else if (clip.Text != "")
            {
                if (ModifierKeys == Keys.Control && Funcs.IsUrl(clip.FullText))
                    Process.Start(clip.FullText);
                else
                    try
                    {
                        Clipboard.SetText(clip.FullText);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Clip appears to be locked.", e.Message, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
            }

            if (!clip.Pinned)
            {
                clip.Save();
                Controls.SetChildIndex(clip, Controls.Count - 1);
            }

            ResumeLayout();
            First();
            _monitorTimer.Enabled = true;
        }

        public void CleanupCache()
        {
            if (Controls.Count >= ClipsConfig.ClipsMaxClips)
            {
                var clipsToDelete = Controls.Count - ClipsConfig.ClipsMaxClips;
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
                DeleteClip((ClipButton)Controls[0]);
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
            if (clip is Image && Controls.Count > 0)
            {
                if (((ClipButton)Controls[Controls.Count - 1]).HasImage && Funcs.IsSame(clip,
                        ((ClipButton)Controls[Controls.Count - 1]).PreviewImageBytes))
                    return (ClipButton)Controls[Controls.Count - 1];
            }
            else if (clip is string && Controls.Count > 0)
            {
                if (((ClipButton)Controls[Controls.Count - 1]).FullText != "" &&
                    ((ClipButton)Controls[Controls.Count - 1]).FullText == clip)
                    return (ClipButton)Controls[Controls.Count - 1];

                foreach (ClipButton b in Controls)
                {
                    if (string.IsNullOrEmpty(b.FileName))
                        continue;

                    if (b.FullText != "" && b.FullText == clip)
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
            var dirCache = Funcs.AppPath() + "\\Cache";

            if (!Directory.Exists(dirCache))
                Directory.CreateDirectory(dirCache);

            var files = Funcs.GetFiles(dirCache, "xml");
            foreach (var file in files) AddClipButton(file, null);
            InLoad = false;
            ResumeLayout();
            ClipsLoaded();
        }

        public void SetMonitorClipboard(bool doMonitorBoard)
        {
            MonitorClipboard = doMonitorBoard;
        }

        #endregion

        #region Overrides & Clipboard hooks

        protected override void Dispose(bool disposing)
        {
            RemoveClipboardFormatListener(Handle);
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                // Why does pasting an image into an outlook email copy the image to the clipboard??

                #region Clipboard hooks

                if (m.Msg == WmClipboardupdate && MonitorClipboard)
                {
                    MonitorClipboard = false;
                    try
                    {
                        var obj = Clipboard.GetDataObject();
                        if (obj == null)
                            return;

                        Funcs.Wait(100); // Occasionally - We were trying to pull data from the clipboard faster than it could give it and it would be blank.
                        // This caused several issues but mainly either a blank image (which we handled) or a clip to be totally ignored.

                        if (Debugger.IsAttached)
                        {
                            var f = obj.GetFormats();
                            Console.WriteLine("Clipboard Ojb Formats: [{0}]", string.Join(", ", f));
                        }

                        if (obj.GetDataPresent(DataFormats.Text))
                        {
                            if (GetClip((string)obj.GetData(DataFormats.Text)) == null)
                                AddClipButton("", ((string)obj.GetData(DataFormats.Text)).Trim());
                        }
                        else if (obj.GetDataPresent(DataFormats.Bitmap))
                        {
                            var f = obj.GetFormats();
                            if (f.Contains("application/x-moz-nativeimage"))
                            {
                                // firefox images are weird, don't always paste into other applications (including whatsapp)
                                // push it back to the clipboard.
                                Clipboard.SetImage(((Bitmap)obj.GetData(DataFormats.Bitmap)));
                                return;
                            }

                            if (GetClip((Bitmap)obj.GetData(DataFormats.Bitmap)) == null)
                                AddClipButton("", (Bitmap)obj.GetData(DataFormats.Bitmap));
                        }
                        else if (obj.GetDataPresent(DataFormats.FileDrop))
                        {
                            var s = string.Join("\n",
                                ((string[])obj.GetData(DataFormats.FileDrop)).Select(i => i.ToString()).ToArray());
                            string[] imageTypes = { ".JPG", ".TIFF", ".TIF", ".BMP", ".PNG", "TIF", ".JPEG" };
                            // Some apps like WhatsApp rather than putting the image in the clipboard will issue a filedrop to the clipboard with the filename.
                            if (File.Exists(s) && imageTypes.Contains(Path.GetExtension(s).ToUpper()))
                                AddClipButton("", new Bitmap(s));
                            else if (GetClip(s) == null)
                                AddClipButton("", s);
                        }
                        else if (obj.GetDataPresent(DataFormats.Dib))
                        {
                            var b = (Bitmap)obj.GetData(DataFormats.Bitmap, true);
                            if (GetClip(b) == null)
                                AddClipButton("", b);
                            b.Dispose();
                        }
                    }
                    finally
                    {
                        MonitorClipboard = true;
                    }
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }

            #endregion
        }

        #endregion
    }
}