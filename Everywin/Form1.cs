using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BrightIdeasSoftware;
using System.IO;


namespace Everywin
{

    public partial class Form1 : Form
    {
        private IntPtr NULL = (IntPtr)0;

        Windows windows;
        KeyboardHook keyboard_hook = new KeyboardHook();
        Shortcut reactivate_shortcut = new Shortcut((uint)Everywin.ModifierKeys.None, Keys.None);

        public Form1()
        {
            InitializeComponent();

            keyboard_hook.KeyPressed +=
            new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);

        }

        public Shortcut GetCurrentShortcut()
        {
            return reactivate_shortcut;
        }

        private void hide_from_user()
        {
            Hide();
        }

        private void jump_to_user()
        {
            this.Show();
            this.Activate();
            this.BringToFront();
            this.Focus();
            this.WindowState = FormWindowState.Normal;
        }
        public void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            jump_to_user();
        }

        public bool SetNewShortcut(Shortcut shortcut)
        {
            bool result = false;
            this.keyboard_hook.UnregisterAllShortcuts();
            result = this.keyboard_hook.RegisterHotKey((Everywin.ModifierKeys)shortcut.Modifiers, shortcut.Key);

            if (!result)
            {
                reactivate_shortcut = new Shortcut();
                return false;
            }
            else
            {
                reactivate_shortcut = shortcut;
                return true;
            }
        }

        public void RemoveShortcut()
        {
            this.keyboard_hook.UnregisterAllShortcuts();
            reactivate_shortcut = new Shortcut();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            windows = new Windows(search_bar, windows_olv);
            windows.Populate();

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void windows_olv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void windows_olv_KeyDown(object sender, KeyEventArgs e)
        {
            //// CTRL + F
            if (e.KeyData == Keys.F && e.Control)
            {
                // focus on search bar
                search_bar.Focus();

                // unselect listbox
                //windows_olv.SelectObject(null);
                //WindowsListbox.SetSelected(WindowsListbox.SelectedIndex, false);
            }
        }

        private void windows_olv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                windows.Enter();
                e.Handled = true;
                return;
            }
            else
            if (e.KeyChar != (char)Keys.Up && e.KeyChar != (char)Keys.Down)
            {
                search_bar.Focus();

                // send keys to search_bar.
                // search bar will bring back focus to us
                SendKeys.Send(e.KeyChar.ToString());

                //windows_olv.Focus();
            }
        }

        private void windows_olv_MouseClick(object sender, MouseEventArgs e)
        {
            //windows.Enter();
        }

        private void search_bar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void search_bar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                windows.Enter();
            }
        }

        private void search_bar_TextChanged(object sender, EventArgs e)
        {
            windows.Search(((TextBox)sender).Text);
        }

        private void search_bar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                windows_olv.Focus();
                windows_olv.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form settings_form = new Form2(this);
            settings_form.Show();
        }

        private void Form1_Enter(object sender, EventArgs e)
        {
            //windows.Populate();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            windows.Populate();
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            jump_to_user();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (Form.ActiveForm == null)
            {
                hide_from_user();
            }
        }
    }
    

}

