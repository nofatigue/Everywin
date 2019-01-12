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
using HWND = System.IntPtr;

namespace Everywin
{
    public partial class Form1 : Form
    {
        Windows windows;

        public Form1()
        {
            InitializeComponent();
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
            }

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
            windows.Enter();
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
    }

    public class Windows
    {
        private TextBox textbox;
        private List<WindowEntry> windows_list;
        private Point text_pos = new Point(0, 0);
        private string current_search_text = "";
        private ObjectListView olv;

        public class WindowEntry
        {
            private string title;
            private IntPtr handle;
            public bool was_drawn = false;
            private string image;
            private string process_path;

            public WindowEntry(string window_title, IntPtr window_handle, string process_path)
            {
                title = window_title;
                handle = window_handle;
                this.process_path = process_path;
                this.image = Path.GetFileName(process_path);

            }

            public string ProcessPath
            {
                get { return process_path; }
            }

            public string Image
            {
                get { return image; }
            }

            public string Title
            {
                get { return title; }
            }

            public IntPtr GetHandle()
            {
                return handle;
            }

            public override string ToString()
            {
                return title;
            }
        }

        public Windows(TextBox search_textbox, ObjectListView olv)
        {
            this.textbox = search_textbox;
            this.olv = olv;
        }

        public void Populate()
        {
            windows_list = new List<WindowEntry>();
            ImageList icons = new ImageList();

            //Process[] processlist = Process.GetProcesses();

            //foreach (Process process in processlist)
            //{
            //    if (!String.IsNullOrEmpty(process.MainWindowTitle))
            //    {
            //        windows_list.Add(new WindowEntry(process.MainWindowTitle, process.MainWindowHandle, process.MainModule.FileName));
            //        Icon icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
            //        icons.Images.Add(process.MainModule.FileName, icon);
            //        //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
            //    }
            //}

            windows_list = OpenWindowGetter.GetOpenWindows();

            foreach (WindowEntry win in windows_list)
            {
                Icon icon = Icon.ExtractAssociatedIcon(win.ProcessPath);
                icons.Images.Add(win.ProcessPath, icon);
            }

            olv.SmallImageList = icons;
            olv.SetObjects(windows_list);
        }

        public void Search(string text)
        {
            //foreach (WindowEntry win in windows_list)
            //{
            //    win.match_search(text);
            //}

            current_search_text = text;

            TextMatchFilter filter = TextMatchFilter.Contains(olv, current_search_text);

            olv.ModelFilter = filter;

            olv.SelectedIndex = 0;
            
            //// Text highlighting requires at least a default renderer
            //if (olv.DefaultRenderer == null)
            //    olv.DefaultRenderer = new HighlightTextRenderer(filter);

            //olv.SetObjects(windows_list);
            //UpdateInterface();

        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void Enter()
        {
            WindowEntry selected_win;

            selected_win = (WindowEntry) olv.SelectedObject;

            SetForegroundWindow(selected_win.GetHandle());

            olv.FindForm().Close();
        }


        /// <summary>Contains functionality to get all the open windows.</summary>
        public static class OpenWindowGetter
        {
            /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
            /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
            public static List<WindowEntry> GetOpenWindows()
            {
                HWND shellWindow = GetShellWindow();
                List<WindowEntry> open_windows = new List<WindowEntry>();
    
            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                string title = builder.ToString();

                string path = GetProcessPath(hWnd);

                open_windows.Add(new WindowEntry(title, hWnd, path));

                return true;

            }, 0);

                return open_windows;
            }

            //WARN: Only for "Any CPU":
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
            public static string GetProcessPath(IntPtr hwnd)
            {
                uint pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);
                if (hwnd != IntPtr.Zero)
                {
                    if (pid != 0)
                    {
                        var process = Process.GetProcessById((int)pid);
                        if (process != null)
                        {
                            return process.MainModule.FileName.ToString();
                        }
                    }
                }
                return "";
            }
            private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

            [DllImport("USER32.DLL")]
            private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

            [DllImport("USER32.DLL" ,CharSet = CharSet.Unicode)]
            private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
            private static extern int GetWindowTextLength(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern bool IsWindowVisible(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern IntPtr GetShellWindow();
        }
    }

    

}

