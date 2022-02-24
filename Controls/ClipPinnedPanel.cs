using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace Clips.Controls
{
    public partial class ClipPinnedPanel : BasePanel
    {
        public ClipPinnedPanel(Config myConfig) : base(myConfig)
        {
            Height = 0;
            ControlAdded += new System.Windows.Forms.ControlEventHandler(ControlsChanged);
            ControlRemoved += new System.Windows.Forms.ControlEventHandler(ControlsChanged);

            Funcs.AddMenuItem(MenuRC, "UnPin", MenuUnPin_Click);
            Funcs.AddMenuItem(MenuRC, "-", null);
            Funcs.AddMenuItem(MenuRC, "Save", MenuSave_Click);
            Funcs.AddMenuItem(MenuRC, "Delete", MenuDelete_Click);

            MonitorTimer = new Timer
            {
                Interval = 200,
                Enabled = false
            };

            MonitorTimer.Tick += new EventHandler(MonitorTimerTick);
        }

        [Obsolete]
        public ClipPinnedPanel()
        {

        }

        private readonly Timer MonitorTimer;

        public void AddClipButton(ClipButton Clip, bool doSave)
        {
            Controls.Add(Clip);
            Clip.OnClipButtonClicked += new ClipButton.ClipButtonClickedHandler(ClipButtonClicked);
            Clip.ContextMenuStrip = MenuRC;
            
            // Set index
            if (doSave)
            {
                Clip.Pinned = true;
                Clip.Save();
            }
        }

        protected override void ClipButtonClicked(ClipButton Clip)
        {
            OnSetClipboardMonitoring?.Invoke(false);      
            base.ClipClicked(Clip);
            if (Clip.HasImage)
            {
                MemoryStream ms = new MemoryStream(Clip.PreviewImageBytes);
                Image img = Image.FromStream(ms);
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
            MonitorTimer.Enabled = true;
        }

        #region Events
        public delegate void ClipUnpinnedHandler(ClipButton Clip);
        public event ClipUnpinnedHandler OnClipUnpinned;

        private void ControlsChanged(object sender, ControlEventArgs e)
        {
            ResizePanel();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            InMenu = true;
            DeleteClip(((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl));
            base.ClipDeleted();
            InMenu = false;
        }

        private void MenuUnPin_Click(object sender, EventArgs e)
        {
            InMenu = true;
            ClipButton b = ((ClipButton)((ClipMenu)((ToolStripMenuItem)sender).Owner).SourceControl);
            Controls.Remove(b);
            b.OnClipButtonClicked -= ClipButtonClicked;
            OnClipUnpinned?.Invoke(b);
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

        public delegate void SetClipboardMonitoring(bool SetMonitorClipboard);
        public event SetClipboardMonitoring OnSetClipboardMonitoring;

        private void MonitorTimerTick(object sender, EventArgs e)
        {
            MonitorTimer.Enabled = false;
            OnSetClipboardMonitoring?.Invoke(true);
        }

        #endregion
        private void ResizePanel()
        {
            int c = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                c += (Controls[i].Height);
            }
            Height = c;
        }
    }
}
