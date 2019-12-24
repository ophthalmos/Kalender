using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Kalender
{
    class ClsBoldedDays
    {
        static readonly string externCalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kalender.ics"); // Assembly.GetExecutingAssembly().GetName().Name
        const int previous = 266; // 2019 - 1753 = 266
        const int future = 250;   // 9998 - 2019 = 7979

        public static Dictionary<DateTime, string> annualDays = new Dictionary<DateTime, string>();
        public static Dictionary<DateTime, string> GetAnnualDays()
        {// für AnnuallyBoldedDates 
            annualDays.Clear();
            annualDays.Add(new DateTime(2000, 1, 1), "Neujahr");
            annualDays.Add(new DateTime(2000, 5, 1), "Maifeiertag");
            annualDays.Add(new DateTime(2000, 10, 3), "Tag der Deutschen Einheit");
            annualDays.Add(new DateTime(2000, 10, 31), "Reformationstag");
            annualDays.Add(new DateTime(2000, 12, 25), "Erster Weihnachtstag");
            annualDays.Add(new DateTime(2000, 12, 26), "Zweiter Weihnachtstag");
            return annualDays;
        }

        public static Dictionary<DateTime, string> GetAnnualDays(int year)
        {// für tooltipFixDays ; if (year > (1753 + 1) && year < (9998 - 3))
            annualDays.Clear();
            for (int i = (year - previous); i <= (year + future); i++)
            {
                annualDays.Add(new DateTime(i, 1, 1), "Neujahr");
                annualDays.Add(new DateTime(i, 5, 1), "Maifeiertag");
                annualDays.Add(new DateTime(i, 10, 3), "Tag der Deutschen Einheit");
                annualDays.Add(new DateTime(i, 10, 31), "Reformationstag");
                annualDays.Add(new DateTime(i, 12, 25), "Erster Weihnachtstag");
                annualDays.Add(new DateTime(i, 12, 26), "Zweiter Weihnachtstag");
            }
            return annualDays;
        }

        public static DateTime GetEasterSunday(int year)
        {
            int c1 = year % 19;
            int c2 = year / 100;
            int c3 = year % 100;
            int c4 = c2 / 4;
            int c5 = c2 % 4;
            int c6 = (c2 + 8) / 25;
            int c7 = (c2 - c6 + 1) / 3;
            int c8 = ((19 * c1) + c2 - c4 - c7 + 15) % 30;
            int c9 = c3 / 4;
            int c10 = c3 % 4;
            int c11 = (32 + (2 * c5) + (2 * c9) - c8 - c10) % 7;
            int c12 = (c1 + (11 * c8) + (22 * c11)) / 451;
            int c13 = c8 + c11 - (7 * c12) + 114;
            int c14 = c13 / 31;        // Monat
            int c15 = (c13 % 31) + 1;    // Tag
            return new DateTime(year, c14, c15);
        }

        public static Dictionary<DateTime, string> mobileDays = new Dictionary<DateTime, string>();
        public static Dictionary<DateTime, string> GetMobileDays(int year, string externEventName)
        {
            mobileDays.Clear(); //  if (year > (1753 + 1) && year < (9998 - 2))
            for (int i = (year - previous); i <= (year + future); i++)
            {
                DateTime easterSunday = GetEasterSunday(i);
                mobileDays.Add(easterSunday.AddDays(-2), "Karfreitag");
                mobileDays.Add(easterSunday.AddDays(1), "Ostermontag");
                mobileDays.Add(easterSunday, "Ostersonntag");
                mobileDays.Add(easterSunday.AddDays(39), "Christi Himmelfahrt");
                mobileDays.Add(easterSunday.AddDays(49), "Pfingstsonntag");
                mobileDays.Add(easterSunday.AddDays(50), "Pfingstmontag");
            }
            if (File.Exists(externCalPath))
            {
                FileStream fs = new FileStream(path: externCalPath, mode: FileMode.Open, access: FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string ical = sr.ReadToEnd();
                char[] delim = { '\n' };
                string[] lines = ical.Split(delim);
                delim[0] = ':';
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("BEGIN:VEVENT"))
                    {
                        if (lines[i + 1].Contains("DTSTART") && lines[i + 2].Contains("DTEND"))
                        {
                            try
                            {
                                DateTime startDate = DateTime.ParseExact(lines[i + 1].Split(delim)[1].Replace("\r", ""), "yyyyMMdd", CultureInfo.InvariantCulture);
                                DateTime endDate = DateTime.ParseExact(lines[i + 2].Split(delim)[1].Replace("\r", ""), "yyyyMMdd", CultureInfo.InvariantCulture);
                                for (DateTime date = startDate; date < endDate; date = date.AddDays(1)) // nur ganze Tage, d.h. endDate mind. nächster Tag
                                {
                                    if ((date.Year.Equals(year) || date.Year.Equals(year + 1)) && !mobileDays.ContainsKey(date)) { mobileDays.Add(date, externEventName); }
                                }
                            }
                            catch (FormatException fe) { Console.WriteLine("{0} Second exception caught.", fe); }
                            catch (ArgumentNullException e) { Console.WriteLine("{0} First exception caught.", e); }
                        }
                        i += 2;
                    }
                }
                sr.Close();
            } 
            return mobileDays;
        }

        public static Dictionary<DateTime, string> MergeBoldedDays(Dictionary<DateTime, string> aDays, Dictionary<DateTime, string> bDays)
        {// to prevent duplicates we’re going to group (using GroupBy) results by keys and pick first value
            Dictionary<DateTime, string> mergedDays = new Dictionary<DateTime, string>();
            mergedDays = aDays.Concat(bDays).GroupBy(i => i.Key).ToDictionary(group => group.Key, group => group.First().Value);
            return mergedDays;
        }
    }
}
