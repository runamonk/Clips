using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clips
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void FormConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OK.PerformClick();
            else
                if (e.KeyCode == Keys.Escape)
                Cancel.PerformClick();
        }

        private void ColorControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                ((Panel)sender).BackColor = dlgColor.Color;
            }
        }

        private void LightTheme_Click(object sender, EventArgs e)
        {
            DarkTheme.Checked = false;
            ClipHeaderColor.BackColor = Color.White;
            ClipBackColor.BackColor = Color.White;
            ClipFontColor.BackColor = Color.Black;
            ClipRowColor.BackColor = Color.White;
            ClipSelected.BackColor = Color.Gray;
            MenuBackColor.BackColor = Color.White;
            MenuBorderColor.BackColor = Color.Gray;
            MenuButtonColor.BackColor = Color.White;
            MenuFontColor.BackColor = Color.Black;
            MenuSelectedColor.BackColor = Color.Gray;
            PreviewBackColor.BackColor = Color.White;
            PreviewFontColor.BackColor = Color.Black;
        }

        private void DarkTheme_Click(object sender, EventArgs e)
        {
            LightTheme.Checked = false;
            ClipHeaderColor.BackColor = Color.FromArgb(56, 56, 56);
            ClipBackColor.BackColor = Color.FromArgb(56, 56, 56);
            ClipFontColor.BackColor = Color.White;
            ClipRowColor.BackColor = Color.FromArgb(56, 56, 56);
            ClipSelected.BackColor = Color.DarkGray;
            MenuBackColor.BackColor = Color.FromArgb(56, 56, 56);
            MenuBorderColor.BackColor = Color.FromArgb(56, 56, 56);
            MenuButtonColor.BackColor = Color.FromArgb(56, 56, 56);
            MenuSelectedColor.BackColor = Color.DarkGray;
            MenuFontColor.BackColor = Color.White;
            MenuSelectedColor.BackColor = Color.DarkGray;
            PreviewBackColor.BackColor = Color.FromArgb(56, 56, 56);
            PreviewFontColor.BackColor = Color.White;
        }

        private void Key_KeyDown(object sender, KeyEventArgs e)
        {
            Keys k = (Keys)e.KeyCode;
            Key.Text = k.ToString();
        }

        private void Key_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // this will stop the key pressed from actually entering into the text box.
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Key.Clear();
        }
    }
}