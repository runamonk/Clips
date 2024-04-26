using System;
using System.IO;
using System.Windows.Forms;

namespace Clips.Controls
{
    public class BasePanel : Panel
    {
        public delegate void ClipAddedHandler(ClipButton clip);

        public delegate void ClipClickedHandler(ClipButton clip);

        public delegate void ClipDeletedHandler();

        public delegate void ClipsLoadedHandler();

        public delegate void ConfigChangedHandler();

        internal readonly ClipMenu MenuRc;

        public BasePanel(Config myConfig)
        {
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += ConfigChanged;

            MenuRc = new ClipMenu(ClipsConfig) { ShowCheckMargin = false, ShowImageMargin = false };
        }

        [Obsolete]
        public BasePanel() { }

        internal Config ClipsConfig { get; set; }
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Hide scrollbars and then enable AutoScroll.
            AutoScroll = false;
            HorizontalScroll.Maximum = 0;
            HorizontalScroll.Visible = false;
            VerticalScroll.Maximum = 0;
            VerticalScroll.Visible = false;
            AutoScroll = true;
            DoubleBuffered = true;
            SetColors();
        }

        protected void ClipAdded(ClipButton clip) { OnClipAdded?.Invoke(clip); }

        protected void ClipClicked(ClipButton clip) { OnClipClicked?.Invoke(clip); }

        protected void ClipDeleted() { OnClipDeleted?.Invoke(); }

        protected void ClipsLoaded() { OnClipsLoaded?.Invoke(); }

        public void DeleteClip(ClipButton clip)
        {
            if (string.IsNullOrEmpty(clip.FileName))
                return;

            if (File.Exists(clip.FileName))
                File.Delete(clip.FileName);

            if (Controls.IndexOf(clip) > -1)
            {
                clip.OnClipButtonClicked -= ClipButtonClicked;
                Controls[Controls.IndexOf(clip)].Dispose();
            }

            ClipDeleted();
        }

        protected virtual void SetColors() { BackColor = ClipsConfig.ClipsBackColor; }

        protected virtual void ClipButtonClicked(ClipButton clip) { }

        protected virtual void ConfigChanged()
        {
            SetColors();
            OnConfigChanged?.Invoke();
        }

        public event ClipAddedHandler OnClipAdded;

        public event ClipClickedHandler OnClipClicked;

        public event ClipDeletedHandler OnClipDeleted;

        public event ClipsLoadedHandler OnClipsLoaded;

        public event ConfigChangedHandler OnConfigChanged;
    }
}