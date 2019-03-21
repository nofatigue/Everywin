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
using System.Threading;

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

            windows = new Windows(search_bar, windows_olv);

            keyboard_hook.KeyPressed +=
            new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);

            reactivate_shortcut = new Shortcut(Properties.Settings.Default.shortcut_modifiers,
                (Keys)Properties.Settings.Default.shortcut_keys);

            SetNewShortcut(reactivate_shortcut);
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

            search_bar.SelectAll();
        }
        public void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            jump_to_user();
        }

        private void open_settings_menu()
        {
            Form settings_form = new Form2(this);
            settings_form.Show();
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
        
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                hide_from_user();
                return true;
            }
            else
            if (Form.ModifierKeys == Keys.None && keyData == Keys.F1)
            {
                open_settings_menu();
            }

            return base.ProcessDialogKey(keyData);
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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr windowHandle,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam,
            SendMessageTimeoutFlags flags,
            uint timeout,
            out IntPtr result);

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x20
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
            if (e.KeyChar == (char)Keys.Up && e.KeyChar == (char)Keys.Down)
            {
                // for selecting items..
            }
            else
            {
                search_bar.Focus();

                // send keys to search_bar.
                // search bar will bring back focus to us
                SendKeys.Send(e.KeyChar.ToString());

                //windows_olv.Focus();
            }
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


        private void Form1_Activated(object sender, EventArgs e)
        {
            windows.Populate();
            search_bar.Focus();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (Form.ActiveForm == null)
            {
                hide_from_user();
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "quit")
            {
                FindForm().Close();
            }
            else
            if (e.ClickedItem.Name == "conf")
            {
                open_settings_menu();
            }
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                jump_to_user();
            }
        }

        private void windows_olv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Delete)
            {
                foreach (var item in windows_olv.SelectedObjects)
                {
                    //SendMessage(((Windows.WindowEntry)item).GetHandle(), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    IntPtr lRes;
                    SendMessageTimeout(((Windows.WindowEntry)item).GetHandle(), WM_CLOSE, IntPtr.Zero, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 10, out lRes);
                }

                Thread.Sleep(10);

                windows.Populate();
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            Form settings_form = new Form2(this);
            settings_form.Show();
        }

        private void windows_olv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            windows.Enter();
        }
    }
    

}

