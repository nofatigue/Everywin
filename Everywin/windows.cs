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
using Everywin.Properties;

namespace Everywin
{
    public class Windows
    {
        private TextBox textbox;
        private List<WindowEntry> windows_list;
        private Point text_pos = new Point(0, 0);
        private string current_search_text = "";
        private ObjectListView olv;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);

        public class WindowEntry
        {
            private IntPtr _handle;

            public WindowEntry(int pid, string windowTitle, IntPtr windowHandle, string processPath, string ramUsage)
            {
                Pid = pid;
                Title = windowTitle;
                _handle = windowHandle;
                ProcessPath = processPath;
                Process = Path.GetFileName(processPath);
                RamUsage = ramUsage;

            }

            public string ProcessPath { get; }

            public string Process { get; }

            public int Pid { get; }
            public string Title { get; }

            public IntPtr GetHandle()
            {
                return _handle;
            }

            public string RamUsage { get; set; }

            public override string ToString()
            {
                return Title;
            }
        }

        public Windows(TextBox search_textbox, ObjectListView olv)
        {
            this.textbox = search_textbox;
            this.olv = olv;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);

        public void Populate()
        {
            windows_list = new List<WindowEntry>();
            ImageList icons = new ImageList();

            windows_list = OpenWindowGetter.GetOpenWindows();

            string my_process_name = Process.GetCurrentProcess().ProcessName + ".exe";

            // filter out our windows
            windows_list = windows_list.Where(win => win.Process != my_process_name).ToList();

            foreach (WindowEntry win in windows_list)
            {
                ushort uicon;
                StringBuilder strB = new StringBuilder(260); // Allocate MAX_PATH chars
                strB.Append(win.ProcessPath);
                IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
                Icon icon = Icon.FromHandle(handle); ;

                // clone icon to a Bitmap object so we can release the handle
                Bitmap bitmap = icon.ToBitmap();

                // need to close icon handle since we used low api
                DestroyIcon(handle);

                icons.Images.Add(win.ProcessPath, bitmap);
            }

            olv.SmallImageList = icons;
            olv.SetObjects(windows_list);
            olv.Refresh();
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
            foreach (WindowEntry selected_win in olv.SelectedObjects)
            {
                ActivateThisWindow(selected_win.GetHandle());
            }

            if (Settings.Default.hide_on_focus_change)
            {
                this.olv.FindForm().Hide();
            }

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

            WINDOWPLACEMENT placement = GetPlacement(handle);

            if (placement.showCmd.HasFlag(ShowWindowState.Minimized))
            {
              
                if (placement.showCmd.HasFlag(ShowWindowState.Maximized))
                {
                    // using SW_RESTORE on maximized windows will change their size,
                    // so use SW_SHOW instead (which seems always to work on maximized windows)
                    ShowWindow(handle, (int)ShowWindowCommands.SW_SHOW);
                }
                else
                {
                    ShowWindow(handle, (int)ShowWindowCommands.SW_RESTORE);
                }
                
            }

            SetForegroundWindow(handle);

            return true;

        }


        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(
            IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowState showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        internal enum ShowWindowState : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        public enum ShowWindowCommands : uint
        {
            /// <summary>
            ///        Hides the window and activates another window.
            /// </summary>
            SW_HIDE = 0,

            /// <summary>
            ///        Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
            /// </summary>
            SW_SHOWNORMAL = 1,

            /// <summary>
            ///        Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
            /// </summary>
            SW_NORMAL = 1,

            /// <summary>
            ///        Activates the window and displays it as a minimized window.
            /// </summary>
            SW_SHOWMINIMIZED = 2,

            /// <summary>
            ///        Activates the window and displays it as a maximized window.
            /// </summary>
            SW_SHOWMAXIMIZED = 3,

            /// <summary>
            ///        Maximizes the specified window.
            /// </summary>
            SW_MAXIMIZE = 3,

            /// <summary>
            ///        Displays a window in its most recent size and position. This value is similar to <see cref="ShowWindowCommands.SW_SHOWNORMAL"/>, except the window is not activated.
            /// </summary>
            SW_SHOWNOACTIVATE = 4,

            /// <summary>
            ///        Activates the window and displays it in its current size and position.
            /// </summary>
            SW_SHOW = 5,

            /// <summary>
            ///        Minimizes the specified window and activates the next top-level window in the z-order.
            /// </summary>
            SW_MINIMIZE = 6,

            /// <summary>
            ///        Displays the window as a minimized window. This value is similar to <see cref="ShowWindowCommands.SW_SHOWMINIMIZED"/>, except the window is not activated.
            /// </summary>
            SW_SHOWMINNOACTIVE = 7,

            /// <summary>
            ///        Displays the window in its current size and position. This value is similar to <see cref="ShowWindowCommands.SW_SHOW"/>, except the window is not activated.
            /// </summary>
            SW_SHOWNA = 8,

            /// <summary>
            ///        Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
            /// </summary>
            SW_RESTORE = 9
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
                        // skip if we don't have privileges for this window
                        return true;
                    }

                    string ramUsage = "N/A";

                    GetWindowThreadProcessId(hWnd, out DWORD pid);
                    try
                    {
                        var process = Process.GetProcessById((int) pid);
                        process.Refresh();
                        ramUsage = SizeFormatHelper.SizeSuffix(process.WorkingSet64, 2);
                    }
                    catch
                    {

                    }

                    open_windows.Add(new WindowEntry((int) pid, title, hWnd, path, ramUsage));

                    return true;

                }, 0);

                foreach (IGrouping<int, WindowEntry> windowEntry in open_windows.GroupBy(entry => entry.Pid))
                {
                    // Mark windows that share PIDs
                    if (windowEntry.Count() > 1)
                    {
                        foreach (var entry in windowEntry)
                        {
                            entry.RamUsage += " (Shared)";
                        }
                    }
                }

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
