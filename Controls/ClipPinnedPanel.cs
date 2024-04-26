using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utility;

namespace Clips.Controls
{
    public class ClipPinnedPanel : BasePanel
    {
        public delegate void ClipUnpinnedHandler(ClipButton clip);

        public delegate void SetClipboardMonitoring(bool setMonitorClipboard);

        private readonly Timer _monitorTimer;

        public ClipPinnedPanel(Config myConfig) : base(myConfig)
        {
            Height = 0;
            ControlAdded += ControlsChanged;
            ControlRemoved += ControlsChanged;

            Funcs.AddMenuItem(MenuRc, "UnPin",  MenuUnPin_Click);
            Funcs.AddMenuItem(MenuRc, "-",      null);
            Funcs.AddMenuItem(MenuRc, "Save",   MenuSave_Click);
            Funcs.AddMenuItem(MenuRc, "Delete", MenuDelete_Click);

            _monitorTimer = new Timer { Interval = 200, Enabled = false };

            _monitorTimer.Tick += MonitorTimerTick;
        }

        [Obsolete]
        public ClipPinnedPanel() { }

        protected override void ClipButtonClicked(ClipButton clip)
        {
            OnSetClipboardMonitoring?.Invoke(false);
            ClipClicked(clip);
            if (clip.HasImage)
            {
                MemoryStream ms = new MemoryStream(clip.PreviewImageBytes);
                Image img = Image.FromStream(ms);

                try
                {
                    Clipboard.SetImage(img);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Clip appears to be locked.", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Clip appears to be locked.", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }

            _monitorTimer.Enabled = true;
        }

        public void AddClipButton(ClipButton clip, bool doSave)
        {
            Controls.Add(clip);
            clip.OnClipButtonClicked += ClipButtonClicked;
            clip.ContextMenuStrip = MenuRc;

            // Set index
            if (doSave)
            {
                clip.Pinned = true;
                clip.Save();
            }
        }

        private void ResizePanel()
        {
            int c = 0;
            for (int i = 0; i < Controls.Count; i++) c += Controls[i].Height;
            Height = c;
        }

        private void ControlsChanged(object sender, ControlEventArgs e) { ResizePanel(); }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            DeleteClip((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl);
            ClipDeleted();
            InMenu = false;
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            InMenu = true;
            SaveFileDialog dlg = new SaveFileDialog { InitialDirectory = "c:\\" };
            ClipButton b = (ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl;

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

        private void MenuUnPin_Click(object sender, EventArgs e)
        {
            InMenu = true;
            ClipButton b = (ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl;
            Controls.Remove(b);
            b.OnClipButtonClicked -= ClipButtonClicked;
            OnClipUnpinned?.Invoke(b);
            InMenu = false;
        }

        private void MonitorTimerTick(object sender, EventArgs e)
        {
            _monitorTimer.Enabled = false;
            OnSetClipboardMonitoring?.Invoke(true);
        }

        public event ClipUnpinnedHandler OnClipUnpinned;

        public event SetClipboardMonitoring OnSetClipboardMonitoring;
    }
}