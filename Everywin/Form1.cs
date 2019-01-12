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

namespace Everywin
{
    public partial class Form1 : Form
    {
        bool setting_show_thumbnails = false;
        Windows windows;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            windows = new Windows(WindowsListbox, search_bar);
            windows.Populate();
        }

        private void search_bar_TextChanged(object sender, EventArgs e)
        {            
            windows.Search(((TextBox)sender).Text);
        }

        private void WindowsListbox_KeyDown(object sender, KeyEventArgs e)
        {
            // CTRL + F
            if (e.KeyCode == Keys.F && e.Control)
            {
                // focus on search bar
                search_bar.Focus();

                // unselect listbox
                WindowsListbox.ClearSelected();
                //WindowsListbox.SetSelected(WindowsListbox.SelectedIndex, false);
            }

            if (e.KeyCode == Keys.Enter)
            {
                windows.Enter();
            }
        }

        private void WindowsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!setting_show_thumbnails)
            {
                return;
            }

            IntPtr thumb;

            Windows.WindowEntry selected = (Windows.WindowEntry)WindowsListbox.SelectedItem;

            int i = DwmRegisterThumbnail(WindowsListbox.FindForm().Handle, selected.GetHandle(), out thumb);

            DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();

            props.fVisible = true;
            props.dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY;
            props.opacity = 255;
            //props.rcDestination = new Rect(preview_picbox.Left, preview_picbox.Top, preview_picbox.Right, preview_picbox.Bottom);
            props.rcDestination = new Rect(0, 0,1000, 1000);
            DwmUpdateThumbnailProperties(thumb, ref props);
        }

        [DllImport("dwmapi.dll")]
        static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);
        [StructLayout(LayoutKind.Sequential)]
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public Rect rcDestination;
            public Rect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PSIZE
        {
            public int x;
            public int y;
        }

        static readonly int DWM_TNP_VISIBLE = 0x8;
        static readonly int DWM_TNP_OPACITY = 0x4;
        static readonly int DWM_TNP_RECTDESTINATION = 0x1;

        private void search_bar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                WindowsListbox.Focus();
                WindowsListbox.SelectedIndex = 0;
            }
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
    }

    public class Windows
    {
        

        private ListBox listbox;
        private TextBox textbox;
        private List<WindowEntry> windows_list;
        private int text_x = 0;
        private int text_y = 0;
        private Point text_pos = new Point(0, 0);
        private string current_search_text = "";
        private bool may_draw_gui = false;
        private bool should_draw = false;

        public class WindowEntry
        {
            private string title;
            private IntPtr handle;
            private int matching_title_part_index;
            public bool was_drawn = false;

            public WindowEntry(string window_title, IntPtr window_handle)
            {
                title = window_title;
                handle = window_handle;
                matching_title_part_index = -1;
            }

            public string GetTitle()
            {
                return title;
            }

            public IntPtr GetHandle()
            {
                return handle;
            }

            public int match_search(string to_match)
            {
                if (to_match == "")
                {
                    matching_title_part_index = -1;
                }
                else
                {
                    matching_title_part_index = title.ToLower().IndexOf(to_match.ToLower());
                }
                
                return matching_title_part_index;
            }

            public int MatchingPartIndex()
            {
                return matching_title_part_index;
            }

            public override string ToString()
            {
                return title;
            }
        }

        public Windows(ListBox window_listbox, TextBox search_textbox)
        {
            listbox = window_listbox;
            textbox = search_textbox;

            // set listbox to use our drawing function (for bold text..)
            //listbox.DrawMode = DrawMode.OwnerDrawFixed;
            listbox.DrawItem += new DrawItemEventHandler(windows_listbox_drawitem);
        }

        public void Populate()
        {
            windows_list = new List<WindowEntry>();

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    windows_list.Add(new WindowEntry(process.MainWindowTitle, process.MainWindowHandle));

                    //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                }
            }

            UpdateInterface();
        }

        public void Search(string text)
        {
            may_draw_gui = false;
            foreach (WindowEntry win in windows_list)
            {
                win.match_search(text);
            }

            current_search_text = text;
          
            UpdateInterface();

            may_draw_gui = true;
        }

        public void UpdateInterface()
        {

            listbox.Items.Clear();
            foreach (WindowEntry win in windows_list)
            {
                if (win.MatchingPartIndex() != -1  || current_search_text == "")
                {
                    win.was_drawn = false;
                    listbox.Items.Add(win);
                }
            }

            //listbox.Update();

        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void Enter()
        {
            WindowEntry selected_win;

            selected_win = (WindowEntry)listbox.SelectedItem;

            SetForegroundWindow(selected_win.GetHandle());

            listbox.FindForm().Close();
        }

        private void windows_listbox_drawitem(object sender, DrawItemEventArgs e)
        {
            
            WindowEntry win_entry = (WindowEntry)((ListBox)(sender)).Items[e.Index];

            //if (((ListBox)(sender)).SelectedIndex != e.Index)
            //{
            //    if (win_entry.was_drawn)
            //    {

            //        e.Graphics.Clear(e.BackColor);
            //        //e.DrawBackground();
            //        //e.DrawFocusRectangle();
            //        #return;
            //    }
            //}

            if (win_entry.was_drawn)
            {

                //e.Graphics.Clear(e.BackColor);
                //e.DrawBackground();
                //e.DrawFocusRectangle();
                //return;
            }

            Font matching_font = new Font("Arial", 10, FontStyle.Bold);
            Font regular_font = new Font("Arial", 10, FontStyle.Regular);
            
            string full_text = ((ListBox)(sender)).Items[e.Index].ToString();

            string[] text_parts;
            Font[] text_fonts;

            int bold_index = win_entry.MatchingPartIndex();

            if (bold_index != -1)
            {
                // SPLIT FOR 3 PARTS
                text_parts = new string[] 
                {   full_text.Substring(0, bold_index),
                    full_text.Substring(bold_index, current_search_text.Length),
                    full_text.Substring(bold_index + current_search_text.Length),
                };

                text_fonts = new Font[]
                {
                    regular_font,
                    matching_font,
                    regular_font
                };

            }
            else
            {
                // ONE PART
                text_parts = new string[]
                {
                    full_text
                };

                text_fonts = new Font[]
                {
                    regular_font
                };

                //TextRenderer.DrawText(e.Graphics, text, font, text_rec, SystemColors.ControlText);
            }

            /*
             * 
             * DRAW TEXT PARTS
             * 
            */

            e.DrawBackground();

            Rectangle text_rec = new Rectangle(e.Bounds.Location, new Size(0, 0));

            foreach (int i in Enumerable.Range(0, text_parts.Length))
             {
                Font font = text_fonts[i];
                string part = text_parts[i];
                Size part_size = TextRenderer.MeasureText(part, font);

                if (part_size.Width >= 5)
                {
                    part_size.Width -= 5;
                }
                
                text_rec.Size = part_size;

                TextRenderer.DrawText(e.Graphics,
                part,
                font,
                text_rec,
                SystemColors.ControlText);

                text_rec.Location += new Size(part_size.Width, 0);
            }
            
            e.DrawFocusRectangle();

            //e.Graphics.DrawString(WindowsListbox.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            win_entry.was_drawn = true;

        }
    }
    
}
