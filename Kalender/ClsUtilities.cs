using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Kalender
{
    internal class Utilities
    {
        internal static bool SetClipboardText(string text)
        {
            try
            {// It retries 5 times with 250 milliseconds between each retry
                Clipboard.SetDataObject(text, false, 5, 250);
                return true;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (ExternalException) { return false; }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        internal static void CenterMouseOverControl(Control ctl, int offsetY) { Cursor.Position = ctl.PointToScreen(new Point((ctl.Width) / 2, ((ctl.Height) / 2) + offsetY)); }

        internal struct DateDiff { public int years, months, days; }

        internal static DateDiff CalcDateDiff(DateTime d1, DateTime d2)
        {// toDate muss immer vor fromDate liegen (toDate < fromDate), ansonsten liefert die Funktion falsche Werte!
            int years, months, days;
            if (d2 < d1) { (d1, d2) = (d2, d1); }; // C# 7 introduced tuples which enables swapping two variables without a temporary one:
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
