using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Kalender
{
    internal static class NativeMethods
    {
        internal enum Modifiers : uint { Alt = 0x0001, Control = 0x0002, Shift = 0x0004, Win = 0x0008 }
        internal const int HOTKEY_ID = 42;
        internal const int WM_HOTKEY = 0x312;
        //internal const int WM_RBUTTONUP = 0x0205;
        //internal const int WM_NCRBUTTONDOWN = 0x00A4; // when the user presses the right mouse button while the cursor is within the nonclient area of a window

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    class Utilities
    {
        internal static bool SetClipboardText(string text)
        {
            try
            {// It retries 5 times with 250 milliseconds between each retry
                Clipboard.SetDataObject(text, false, 5, 250);
                return true;
            }
            catch { return false; }
        }

        internal static void CenterMouseOverControl(Control ctl, int offsetY)
        {// -45 to put it to ToDayDate; Cursor.Position = contextMenu.PointToScreen(new Point(contextMenu.Width / 2, 10));
            Cursor.Position = ctl.PointToScreen(new Point((ctl.Width) / 2, ((ctl.Height) / 2) + offsetY));
        }

        internal struct DateDiff { public int years, months, days; }

        internal static DateDiff CalcDateDiff(DateTime d1, DateTime d2)
        {// toDate muss immer vor fromDate liegen (toDate < fromDate), ansonsten liefert die Funktion falsche Werte!
            int years, months, days;
            if (d2 < d1)
            {
                DateTime foo = d1;
                d1 = d2;
                d2 = foo;
            }
            years = d2.Year - d1.Year;
            DateTime dt = d1.AddYears(years);
            if (dt > d2)
            {
                years--;
                dt = d1.AddYears(years);
            }
            months = d2.Month - d1.Month;
            if (d2.Day < d1.Day) months--;
            months = (months + 12) % 12;
            dt = dt.AddMonths(months);
            days = (d2 - dt).Days;
            DateDiff ddf;
            ddf.years = years; ddf.months = months; ddf.days = days;
            return (ddf);
        }

    }
}
