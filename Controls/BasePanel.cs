using System;
using System.Windows.Forms;

namespace Clips.Controls
{
    public partial class BasePanel : Panel
    {
        public BasePanel(Config myConfig)
        {
            VerticalScroll.Enabled = true;
            HorizontalScroll.Enabled = false;
            AutoScroll = true;
            DoubleBuffered = true;

            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new ConfigChangedHandler(ConfigChanged);
            PreviewForm = new Preview(ClipsConfig);    
        }

        #region Properties
        internal Config ClipsConfig { get; set; }
        internal bool InPreview { get; set; }
        #endregion

        #region Privates
        internal readonly Preview PreviewForm;
        #endregion

        #region Events
        public delegate void ConfigChangedHandler();
        public event ConfigChangedHandler OnConfigChanged;

        public delegate void ClipAddedHandler(ClipButton Clip);
        public event ClipAddedHandler OnClipAdded;

        public delegate void ClipClickedHandler(ClipButton Clip);
        public event ClipClickedHandler OnClipClicked;

        public delegate void ClipDeletedHandler();
        public event ClipDeletedHandler OnClipDeleted;

        public delegate void ClipsLoadedHandler();
        public event ClipsLoadedHandler OnClipsLoaded;

        protected virtual void ConfigChanged()
        {
            SetColors();
            OnConfigChanged?.Invoke();
        }

        protected void ClipAdded(ClipButton Clip)
        {
            OnClipAdded?.Invoke(Clip);
        }

        protected void ClipClicked(ClipButton Clip)
        {
            OnClipClicked?.Invoke(Clip);
        }

        protected void ClipDeleted()
        {
            OnClipDeleted?.Invoke();
        }

        protected void ClipsLoaded()
        {
            OnClipsLoaded?.Invoke();
        }
        #endregion

        internal void PreviewHide(object sender, EventArgs e)
        {
            PreviewForm.HidePreview();
            InPreview = false;
        }

        internal void PreviewShow(object sender, EventArgs e)
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
