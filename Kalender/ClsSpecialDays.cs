using System;
using System.Collections.Generic;

namespace Kalender
{
    internal class ClsAnnualDays : Dictionary<GermanAnnualDayKey, GermanAnnualDay>
    {// Auflistung zur Verwaltung der speziellen Tage in Deutschland
        internal ClsAnnualDays() {  }// Konstruktor
        internal void Add(GermanAnnualDayKey key, string name, DateTime date, bool nationWide, bool holiday) { base.Add(key, new GermanAnnualDay(key, name, date, nationWide, holiday)); }
    }

    internal class ClsSpecialDays : Dictionary<GermanSpecialDayKey, GermanSpecialDay>
    {// Auflistung zur Verwaltung der speziellen Tage in Deutschland
        internal int year;
        public int Year { get { return this.year; } } // Gibt das Jahr zurück, für das diese speziellen Tage gelten
        internal ClsSpecialDays(int year) { this.year = year; } // Konstruktor

        internal void Add(GermanSpecialDayKey key, string name, DateTime date, bool nationWide, bool holiday) { base.Add(key, new GermanSpecialDay(key, name, date, nationWide, holiday)); }
    }

    public enum GermanAnnualDayKey
    {
        Neujahr,
        Maifeiertag,
        MariaHimmelfahrt,
        TagDerDeutschenEinheit,
        Reformationstag,
        ErsterWeihnachtstag,
        ZweiterWeihnachtstag,
    }

    public enum GermanSpecialDayKey
    {
        Karfreitag,
        Ostersonntag,
        Ostermontag,
        ChristiHimmelfahrt,
        Pfingstsonntag,
        Pfingstmontag,
    }

    public class GermanAnnualDay : IComparable<GermanAnnualDay>
    {// Verwaltet einen speziellen deutschen Tag       
        public GermanAnnualDayKey Key; // Der Schlüssel des speziellen Tags
        public string Name; // Der Name des speziellen Tags
        public DateTime Date; // Das Datum des speziellen Tags
        public bool IsNationwide; // Gibt an, ob der spezielle Tag bundesweit gilt
        public bool IsHoliday; // Gibt an, ob es sich bei dem speziellen Tag

        public GermanAnnualDay(GermanAnnualDayKey key, string name, DateTime date, bool isNationwide, bool isHoliday)
        {
            Key = key;
            Name = name;
            Date = date;
            IsNationwide = isNationwide;
            IsHoliday = isHoliday;
        }
        public int CompareTo(GermanAnnualDay otherAnnualDay) { return Date.CompareTo(otherAnnualDay.Date); }
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
            Key = key;
            Name = name;
            Date = date;
            IsNationwide = isNationwide;
            IsHoliday = isHoliday;
        }

        public int CompareTo(GermanSpecialDay otherSpecialDay) { return Date.CompareTo(otherSpecialDay.Date); }
    }


    public static partial class GermanAnnualDays
    {

        internal static ClsAnnualDays GetAnnualDays()
        {// GermanSpecialDays-Instanz erzeugen
            int year = 1999;
            ClsAnnualDays gad = new ClsAnnualDays()
            { //                                                                                              isNationwide, isHoliday                      
                {GermanAnnualDayKey.Neujahr,                "Neujahr",                   new DateTime(year, 1, 1),   true,  true },
                {GermanAnnualDayKey.Maifeiertag,            "Maifeiertag",               new DateTime (year, 5, 1),  true,  true },
                {GermanAnnualDayKey.TagDerDeutschenEinheit, "Tag der Deutschen Einheit", new DateTime(year, 10, 3),  true,  true },
                {GermanAnnualDayKey.Reformationstag,        "Reformationstag",           new DateTime(year, 10, 31), true,  true },
                {GermanAnnualDayKey.ErsterWeihnachtstag,    "Erster Weihnachtstag",      new DateTime(year, 12, 25), true,  true },
                {GermanAnnualDayKey.ZweiterWeihnachtstag,   "Zweiter Weihnachtstag",     new DateTime(year, 12, 26), true,  true },
            };
            return gad;
        }
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
            };

            DateTime easterSunday = GetEasterSundayDate(year);
            gsd.Add(GermanSpecialDayKey.Karfreitag, "Karfreitag", easterSunday.AddDays(-2), true, true);
            gsd.Add(GermanSpecialDayKey.Ostermontag, "Ostermontag", easterSunday.AddDays(1), true, true);
            gsd.Add(GermanSpecialDayKey.Ostersonntag, "Ostersonntag", easterSunday, true, true);
            gsd.Add(GermanSpecialDayKey.ChristiHimmelfahrt, "Christi Himmelfahrt", easterSunday.AddDays(39), true, true);
            gsd.Add(GermanSpecialDayKey.Pfingstsonntag, "Pfingstsonntag", easterSunday.AddDays(49), true, true);
            gsd.Add(GermanSpecialDayKey.Pfingstmontag, "Pfingstmontag", easterSunday.AddDays(50), true, true);
            return gsd;
        }
    }
}
