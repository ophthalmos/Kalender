using System;
using System.Collections; // ArrayList
using System.Runtime.InteropServices;
using System.Text;

namespace Kalender
{
    internal static class NativeMethods
    {
        internal enum Modifiers : uint { Alt = 0x0001, Control = 0x0002, Shift = 0x0004, Win = 0x0008 }
        internal const int HOTKEY_ID = 42;
        internal const int WM_HOTKEY = 0x312;
        internal const int GWL_EXSTYLE = (-20);
        internal const int GW_OWNER = 4;
        internal const uint WS_EX_TOOLWINDOW = 0x00000080;
        internal const uint WS_EX_APPWINDOW = 0x40000; // provides a taskbar button for a window that would otherwise lack one
        internal const int DWMWA_CLOAKED = 14;
        //internal const uint MCM_SETCOLOR = 0x100A;
        //internal const int MCSC_BACKGROUND = 0; // background between months in MonthCalendar

        internal delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam); // stimmt mit 2x IntPtr
        internal static ArrayList windowHandles = new ArrayList();

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam); //I think this is what you need the lparam determines what you are setting and wparam determines the color.

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal extern static int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        [DllImport("DwmApi.dll")]
        internal static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttributeToGet, out int pvAttributeValue, int cbAttribute);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        internal extern static int EnumWindows(EnumWindowsCallback hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, int flags);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        internal static extern int GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] // Win32 does not support GetWindowLongPtr directly. Problem wird durch Abfrage umgangen (s.u.)
        internal static extern int GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        internal static string GetActiveWindowClass(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (GetClassName(handle, Buff, nChars) > 0) { return Buff.ToString(); }
            return null;
        }

        public static IntPtr GetLastWinHandle(IntPtr exclHwnd)
        {
            IntPtr ownerHwnd = IntPtr.Zero;
            IntPtr foundHwnd = IntPtr.Zero;
            windowHandles.Clear();
            EnumWindows(new EnumWindowsCallback(EnumCallback), IntPtr.Zero);
            foreach (IntPtr indexHwnd in windowHandles)
            {
                if (IsWindowVisible(indexHwnd) && indexHwnd != exclHwnd)
                {
                    ownerHwnd = indexHwnd;
                    do
                    {
                        ownerHwnd = GetWindow(ownerHwnd, GW_OWNER);
                    } while (!IntPtr.Zero.Equals(GetWindow(ownerHwnd, GW_OWNER)));
                    ownerHwnd = ownerHwnd != IntPtr.Zero ? ownerHwnd : indexHwnd;
                    if (GetLastActivePopup(ownerHwnd) == indexHwnd)
                    {
                        int es = IntPtr.Size == 8 ? GetWindowLongPtr64(indexHwnd, GWL_EXSTYLE) : GetWindowLongPtr32(indexHwnd, GWL_EXSTYLE);
                        if ((!(((es & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW) && ((es & WS_EX_APPWINDOW) != WS_EX_APPWINDOW))) && !IsInvisibleWin10BackgroundAppWindow(indexHwnd))
                        {
                            foundHwnd = indexHwnd;
                            break;
                        }
                    }
                }
            }
            return foundHwnd;
        }

        private static bool EnumCallback(IntPtr hWnd, IntPtr lParam)
        {
            windowHandles.Add(hWnd);
            return true;
        }

        private static bool IsInvisibleWin10BackgroundAppWindow(IntPtr hWindow)
        {
            int hr = DwmGetWindowAttribute(hWindow, DWMWA_CLOAKED, out int cloakedVal, sizeof(int));
            if (hr != 0) { cloakedVal = 0; } // returns S_OK (which is zero) on success. Otherwise, it returns an HRESULT error code
            return cloakedVal != 0;
        }
    }
}
