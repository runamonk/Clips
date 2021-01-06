using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clips.Controls
{
    public partial class ClipMenu : ContextMenuStrip

    {
        private Config ClipsConfig { get; set; }

        public ClipMenu(Config myConfig)
        {
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new EventHandler(ConfigChanged);
            SetColors();
        }

        private void SetColors()
        {
            BackColor = ClipsConfig.MenuBackColor;
            ForeColor = ClipsConfig.MenuFontColor;
            Renderer = null;
            Renderer = new CustomToolstripRenderer(ClipsConfig);
        }
        private void ConfigChanged(object sender, EventArgs e)
        {
            SetColors();
        }
    }
   
    public class CustomToolstripRenderer : ToolStripProfessionalRenderer
    {
        public CustomToolstripRenderer(Config MyConfig) : base(new CustomColors(MyConfig)) { }
    }

    public class CustomColors : ProfessionalColorTable
    {
        Config config;
        public CustomColors(Config MyConfig)
        {
            config = MyConfig;
        }

        public override Color ButtonSelectedBorder
        {
            get { return Color.Transparent; }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return config.MenuBackColor; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return config.MenuBackColor; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return config.MenuBackColor; }
        }
        public override Color MenuItemSelected
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color MenuItemBorder
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color MenuBorder
        {
            get { return config.MenuBorderColor; }
        }
        public override Color CheckSelectedBackground
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color CheckBackground
        {
            get { return config.MenuBackColor; }
        }
        public override Color CheckPressedBackground
        {
            get { return config.MenuBackColor; }
        }
    }
}
