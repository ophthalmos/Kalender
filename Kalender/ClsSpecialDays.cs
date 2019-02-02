using System;
using System.Collections.Generic;

namespace Kalender
{
    internal class ClsSpecialDays : Dictionary<GermanSpecialDayKey, GermanSpecialDay>
    {// Auflistung zur Verwaltung der speziellen Tage in Deutschland
        internal int year;
        public int Year { get { return this.year; } } // Gibt das Jahr zurück, für das diese speziellen Tage gelten
        internal ClsSpecialDays(int year) { this.year = year; } // Konstruktor

        internal void Add(GermanSpecialDayKey key, string name, DateTime date, bool nationWide, bool holiday) { base.Add(key, new GermanSpecialDay(key, name, date, nationWide, holiday)); }
    }

    public enum GermanSpecialDayKey
    {
        Neujahr,
        HeiligeDreiKönige,
        Maifeiertag,
        MariaHimmelfahrt,
        TagDerDeutschenEinheit,
        Reformationstag,
        Allerheiligen,
        HeiligerAbend,
        ErsterWeihnachtstag,
        ZweiterWeihnachtstag,
        Rosenmontag,
        Aschermittwoch,
        Gründonnerstag,
        Karfreitag,
        Ostersonntag,
        Ostermontag,
        ChristiHimmelfahrt,
        Pfingstsonntag,
        Pfingstmontag,
        Fronleichnam,
        ErsterAdvent,
        ZweiterAdvent,
        DritterAdvent,
        VierterAdvent,
        Totensonntag,
        BußUndBettag
    }

    public class GermanSpecialDay : IComparable<GermanSpecialDay>
    {// Verwaltet einen speziellen deutschen Tag       
        public GermanSpecialDayKey Key; // Der Schlüssel des speziellen Tags
        public string Name; // Der Name des speziellen Tags
        public DateTime Date; // Das Datum des speziellen Tags
        public bool IsNationwide; // Gibt an, ob der spezielle Tag bundesweit gilt
        public bool IsHoliday; // Gibt an, ob es sich bei dem speziellen Tag

        public GermanSpecialDay(GermanSpecialDayKey key, string name, DateTime date, bool isNationwide, bool isHoliday)
        {
            this.Key = key;
            this.Name = name;
            this.Date = date;
            this.IsNationwide = isNationwide;
            this.IsHoliday = isHoliday;
        }

        public int CompareTo(GermanSpecialDay otherSpecialDay) { return Date.CompareTo(otherSpecialDay.Date); }
    }

    public static partial class GermanSpecialDays
    {
        public static DateTime GetEasterSundayDate(int year)
        {
            int c1 = year % 19;
            int c2 = year / 100;
            int c3 = year % 100;
            int c4 = c2 / 4;
            int c5 = c2 % 4;
            int c6 = (c2 + 8) / 25;
            int c7 = (c2 - c6 + 1) / 3;
            int c8 = (19 * c1 + c2 - c4 - c7 + 15) % 30;
            int c9 = c3 / 4;
            int c10 = c3 % 4;
            int c11 = (32 + 2 * c5 + 2 * c9 - c8 - c10) % 7;
            int c12 = (c1 + 11 * c8 + 22 * c11) / 451;
            int c13 = c8 + c11 - 7 * c12 + 114;
            int c14 = c13 / 31;        // Monat
            int c15 = c13 % 31 + 1;    // Tag
            return new DateTime(year, c14, c15);
        }

        internal static ClsSpecialDays GetGermanSpecialDays(int year)
        {// GermanSpecialDays-Instanz erzeugen
            ClsSpecialDays gsd = new ClsSpecialDays(year)
            { //                                                                                              isNationwide, isHoliday                      
                {GermanSpecialDayKey.Neujahr,                "Neujahr",                   new DateTime(year, 1, 1),   true,  true },
                {GermanSpecialDayKey.HeiligeDreiKönige,      "Heilige Drei Könige",       new DateTime(year, 1, 6),   false, false},
                {GermanSpecialDayKey.Maifeiertag,            "Maifeiertag",               new DateTime (year, 5, 1),  true,  true },
                {GermanSpecialDayKey.MariaHimmelfahrt,       "Maria Himmelfahrt",         new DateTime(year, 8, 15),  false, false},
                {GermanSpecialDayKey.TagDerDeutschenEinheit, "Tag der Deutschen Einheit", new DateTime(year, 10, 3),  true,  true },
                {GermanSpecialDayKey.Reformationstag,        "Reformationstag",           new DateTime(year, 10, 31), true,  true },
                {GermanSpecialDayKey.Allerheiligen,          "Allerheiligen",             new DateTime(year, 11, 1),  false, false},
                {GermanSpecialDayKey.HeiligerAbend,          "Heiliger Abend",            new DateTime(year, 12, 24), true,  false},
                {GermanSpecialDayKey.ErsterWeihnachtstag,    "Erster Weihnachtstag",      new DateTime(year, 12, 25), true,  true },
                {GermanSpecialDayKey.ZweiterWeihnachtstag,   "Zweiter Weihnachtstag",     new DateTime(year, 12, 26), true,  true },
            };

            DateTime easterSunday = GetEasterSundayDate(year);
            gsd.Add(GermanSpecialDayKey.Rosenmontag, "Rosenmontag", easterSunday.AddDays(-48), true, false);
            gsd.Add(GermanSpecialDayKey.Aschermittwoch, "Aschermittwoch", easterSunday.AddDays(-46), true, false);
            gsd.Add(GermanSpecialDayKey.Gründonnerstag, "Gründonnerstag", easterSunday.AddDays(-3), true, false);
            gsd.Add(GermanSpecialDayKey.Karfreitag, "Karfreitag", easterSunday.AddDays(-2), true, true);
            gsd.Add(GermanSpecialDayKey.Ostermontag, "Ostermontag", easterSunday.AddDays(1), true, true);
            gsd.Add(GermanSpecialDayKey.Ostersonntag, "Ostersonntag", easterSunday, true, true);
            gsd.Add(GermanSpecialDayKey.ChristiHimmelfahrt, "Christi Himmelfahrt", easterSunday.AddDays(39), true, true);
            gsd.Add(GermanSpecialDayKey.Pfingstsonntag, "Pfingstsonntag", easterSunday.AddDays(49), true, true);
            gsd.Add(GermanSpecialDayKey.Pfingstmontag, "Pfingstmontag", easterSunday.AddDays(50), true, true);
            gsd.Add(GermanSpecialDayKey.Fronleichnam, "Fronleichnam", easterSunday.AddDays(60), false, false);
            DateTime firstXMasDay = new DateTime(year, 12, 25);
            DateTime fourthAdvent;
            int weekday = (int)firstXMasDay.DayOfWeek;
            if (weekday == 0) { fourthAdvent = firstXMasDay.AddDays(-7); } // Sonntag
            else { fourthAdvent = firstXMasDay.AddDays(-weekday); }
            gsd.Add(GermanSpecialDayKey.VierterAdvent, "Vierter Advent", fourthAdvent, true, false);
            gsd.Add(GermanSpecialDayKey.DritterAdvent, "Dritter Advent", fourthAdvent.AddDays(-7), true, false);
            gsd.Add(GermanSpecialDayKey.ZweiterAdvent, "Zweiter Advent", fourthAdvent.AddDays(-14), true, false);
            gsd.Add(GermanSpecialDayKey.ErsterAdvent, "Erster Advent", fourthAdvent.AddDays(-21), true, false);
            DateTime deadSunday = fourthAdvent.AddDays(-28);
            gsd.Add(GermanSpecialDayKey.Totensonntag, "Totensonntag", deadSunday, true, false);
            gsd.Add(GermanSpecialDayKey.BußUndBettag, "Buß- und Bettag", deadSunday.AddDays(-(weekday + 4)), false, false);

            return gsd;
        }
    }
}
