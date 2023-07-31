using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalender
{
    internal class Utilities
    {
        internal static async Task HttpGetGoogleCalendar(string urlGoogleCalendar, string fileToWriteTo)
        {
            using (HttpClient client = new HttpClient()) // wird nur 1x im Load-Event aufgerufen - deshalb using ok
            {
                try
                {
                    using (HttpResponseMessage response = await client.GetAsync(urlGoogleCalendar, HttpCompletionOption.ResponseHeadersRead))
                        if (response.IsSuccessStatusCode)
                        {
                            using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                            {
                                using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create)) { await streamToReadFrom.CopyToAsync(streamToWriteTo); }
                            }
                        }
                }
                catch (Exception ex) when (ex is WebException || ex is HttpRequestException || ex is ArgumentException) { MessageBox.Show("Die Google-Kalenderdaten konnten nicht aktualisiert werden." + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        internal static bool IsAutoStartEnabled(string taskName)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "schtasks.exe", // Specify exe name.
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/query /TN \"" + taskName + "\"",
                RedirectStandardOutput = true
            };
            using (Process process = Process.Start(start))
            {// Read in all the text from the process with the StreamReader.
                using (StreamReader reader = process.StandardOutput)
                {
                    string stdout = reader.ReadToEnd();
                    if (stdout.Contains(taskName)) { return true; }
                    else { return false; }
                }
            }
        }

        internal static void SetAutoStart(string appName, string assemblyLocation)
        {
            var p = new Process
            {
                StartInfo = {
                  UseShellExecute = false,
                  FileName = "SCHTASKS.exe",
                  RedirectStandardError = true,
                  RedirectStandardOutput = true,
                  CreateNoWindow = true,
                  WindowStyle = ProcessWindowStyle.Hidden,
                  //Arguments = string.Format(@"/Create /F /RL HIGHEST /SC ONLOGON /DELAY 0000:45 /TN " + appName + " /TR \"" + assemblyLocation + "\"")
                  Arguments = string.Format(@"/Create /F /RL HIGHEST /SC ONLOGON /DELAY 0000:45 /TN " + appName + " /TR \"'" + assemblyLocation + "'\"")
               }
            };
            p.Start();
        }

        internal static void UnSetAutoStart(string taskName)
        {
            var p = new Process
            {
                StartInfo = {
                  UseShellExecute = false,
                  FileName = "SCHTASKS.exe",
                  RedirectStandardError = true,
                  RedirectStandardOutput = true,
                  CreateNoWindow = true,
                  WindowStyle = ProcessWindowStyle.Hidden,
                  Arguments = string.Format(@"/Delete /F /TN " + taskName)
               }
            };
            p.Start();
        }

        internal static bool SetClipboardText(string text)
        {
            try
            {// It retries 5 times with 250 milliseconds between each retry
                Clipboard.SetDataObject(text, false, 5, 250);
                return true;
            }
            catch (Exception ex) when (ex is ExternalException) { return false; }
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
            if (months == 1) { dt = dt.AddMonths(-1); months = 0; } // 30.8.20 neu eingefügt
            days = (d2 - dt).Days;
            DateDiff ddf;
            ddf.years = years; ddf.months = months; ddf.days = days;
            return (ddf);
        }

    }
}
