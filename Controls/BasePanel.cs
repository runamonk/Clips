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
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);
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
        internal event EventHandler OnConfigChanged;

        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
            OnConfigChanged?.Invoke(sender, e);
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
