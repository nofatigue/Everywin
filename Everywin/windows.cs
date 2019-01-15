using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HWND = System.IntPtr;
using DWORD = System.UInt32;
using LPDWORD = System.IntPtr;
using BOOL = System.UInt32;
using System.Diagnostics;
using System.Threading;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Everywin
{
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

            windows_list = OpenWindowGetter.GetOpenWindows();

            string my_process_name = Process.GetCurrentProcess().ProcessName + ".exe";

            // filter out our windows
            windows_list = windows_list.Where(win => win.Image != my_process_name).ToList();

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
            current_search_text = text;

            TextMatchFilter filter = TextMatchFilter.Contains(olv, current_search_text);

            olv.ModelFilter = filter;

            olv.SelectedIndex = 0;

        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern BOOL ShowWindow(
  HWND hWnd,
  int nCmdShow
);
        public void Enter()
        {
            WindowEntry selected_win;

            selected_win = (WindowEntry)olv.SelectedObject;

            ActivateThisWindow(selected_win.GetHandle());

            //this.olv.FindForm().WindowState = FormWindowState.Minimized;
            //this.olv.FindForm().SendToBack();

            this.olv.FindForm().Hide();
            //this.olv.FindForm().Show();
            
        }

        [DllImport("kernel32.dll")]
        private static extern DWORD GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern DWORD GetWindowThreadProcessId(HWND hWnd, LPDWORD lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern BOOL AttachThreadInput(
  DWORD idAttach,
  DWORD idAttachTo,
  BOOL fAttach
);

        [DllImport("user32.dll")]
        private static extern HWND SetActiveWindow(
  HWND hWnd
);
        private bool ActivateThisWindow(HWND handle)
        {
            DWORD currentThreadId = GetCurrentThreadId();
            DWORD otherThreadId = GetWindowThreadProcessId(handle, (IntPtr)0);
            if (otherThreadId == 0) return false;
            if (otherThreadId != currentThreadId)
            {
                AttachThreadInput(currentThreadId, otherThreadId, 1);
            }

            SetActiveWindow(handle);

            if (otherThreadId != currentThreadId)
            {
                AttachThreadInput(currentThreadId, otherThreadId, 0);
            }

            // SW_RESTORE
            ShowWindow(handle, 9);
            return true;

            //SetForegroundWindow(selected_win.GetHandle());
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
                    string path;

                    try
                    {
                        path = GetProcessPath(hWnd);
                    }

                    catch (System.ComponentModel.Win32Exception)
                    {
                        // skip if we don't have priviliages for this window
                        return true;
                    }

                    open_windows.Add(new WindowEntry(title, hWnd, path));

                    return true;

                }, 0);

                return open_windows;
            }

            // old implementation 
            //public static string GetProcessPath(IntPtr hwnd)
            //{
            //    uint pid = 0;
            //    GetWindowThreadProcessId(hwnd, out pid);
            //    if (hwnd != IntPtr.Zero)
            //    {
            //        if (pid != 0)
            //        {
            //            var process = Process.GetProcessById((int)pid);
            //            if (process != null)
            //            {
            //                return process.MainModule.FileName.ToString();
            //            }
            //        }
            //    }
            //    return "";
            //}

           
            public static string GetProcessPath(IntPtr hwnd)
            {
                DWORD pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);
                HWND process_handle = (HWND)0;

                try
                {
                    process_handle = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, pid);
                    if (process_handle == (HWND)0)
                    {
                        return null;
                    }

                    var fileNameBuilder = new StringBuilder(1024);
                    uint bufferLength = (uint)fileNameBuilder.Capacity + 1;

                    return QueryFullProcessImageName(process_handle, 0, fileNameBuilder, ref bufferLength) ?
                    fileNameBuilder.ToString() :
                    null;


                }                    
                finally
                {
                    if (process_handle != (HWND)0)
                    {
                        CloseHandle(process_handle);
                    }
                }
                
            }
            private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

            [DllImport("USER32.DLL")]
            private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

            [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
            private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
            private static extern int GetWindowTextLength(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern bool IsWindowVisible(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern IntPtr GetShellWindow();

            //WARN: Only for "Any CPU":
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
            [DllImport("kernel32.dll", SetLastError = true)]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [SuppressUnmanagedCodeSecurity]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenProcess(
     ProcessAccessFlags processAccess,
     bool bInheritHandle,
     DWORD processId
);[Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VirtualMemoryOperation = 0x00000008,
                VirtualMemoryRead = 0x00000010,
                VirtualMemoryWrite = 0x00000020,
                DuplicateHandle = 0x00000040,
                CreateProcess = 0x000000080,
                SetQuota = 0x00000100,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                QueryLimitedInformation = 0x00001000,
                Synchronize = 0x00100000
            }

            [DllImport("Kernel32.dll")]
            private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        }
    }
}
