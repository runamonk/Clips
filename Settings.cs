using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clips
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FormConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
            else
                if (e.KeyCode == Keys.Escape)
                    btnCancel.PerformClick();
        }

        private void TextHotkey_KeyDown(object sender, KeyEventArgs e)
        {           
            Keys k = (Keys)e.KeyCode;
            textHotkey.Text = k.ToString();
         }

        private void TextHotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // this will stop the key pressed from actually entering into the text box.
        }

        private void ColorControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                ((Panel)sender).BackColor = dlgColor.Color;
            }
        }

        private void rbDark_Click(object sender, EventArgs e)
        {
            rbLight.Checked = false;
        }

        private void rbLight_Click(object sender, EventArgs e)
        {
            rbDark.Checked = false;
        }
    }
}
