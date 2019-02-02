using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kalender
{
    public partial class CmpCalendar : MonthCalendar
    {
        //List<DateTime> WarningDates = new List<DateTime>();
        private ClsSpecialDays holidays;
        internal Timer timer; // Timer für das zeitverzögerte Ermitteln der speziellen Tage
        internal ToolTip toolTip = new ToolTip(); // ToolTip für die Anzeige der Feiertage
        private Point lastMouseLocation = new Point(-1, -1); // Die letzte Mausposition. Wird zum Verhindern von Flackern benötigt.

        //protected static int WM_PAINT = 0x000F;
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{// Override WndProc and force a call to OnPaint when we get a WM_PAINT 
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_PAINT)
        //    {
        //        Graphics graphics = Graphics.FromHwnd(this.Handle);
        //        PaintEventArgs pe = new PaintEventArgs(graphics, new Rectangle(0, 0, this.Width, this.Height));
        //        OnPaint(pe);
        //    }
        //}

        public CmpCalendar()
        {
            //DateTime today = DateTime.Today;
            //WarningDates.Add(today.AddDays(-1));
            //WarningDates.Add(today.AddDays(-2));
            //WarningDates.Add(today.AddDays(12));
            //WarningDates.Add(today.AddMonths(1));
            //// Konstruktor. Initialisiert den Timer und ermittelt die ersten Feiertage
            this.timer = new Timer { Interval = 100 };
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.GetHolidays(this.SelectionStart.Year);
        }

        protected override void OnDateChanged(DateRangeEventArgs e)
        { // Ermittelt die speziellen Tage neu, wenn das Jahr wechselt
            base.OnDateChanged(e);
            this.GetHolidays(e.Start.Year);
        }

        private void GetHolidays(int year)
        {
            if (this.holidays == null || this.holidays.Year != year)
            {//Die speziellen Tage für das angezeigte Jahr ermitteln
                ClsSpecialDays specialDays = DateAndTimeUtils.GetGermanSpecialDays(year);
                this.holidays = new ClsSpecialDays(year);
                foreach (var specialDay in specialDays.Values)
                {
                    if (specialDay.IsHoliday) { this.holidays.Add(specialDay.Key, specialDay); }
                }
                this.timer.Start(); // Timer starten, der etwas zeitverzögert die Feiertage ermittelt und in BoldedDates schreibt
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {// Schreibt etwas zeitverzögert die Feiertage in die BoldedDates-Eigenschaft
            this.timer.Stop();
            DateTime[] specialDates = new DateTime[this.holidays.Count];
            int index = -1;
            foreach (var specialDay in this.holidays.Values)
            {
                index++;
                specialDates[index] = specialDay.Date;
            }
            this.BoldedDates = specialDates;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {// Zeigt einen ToolTip an, wenn sich die Maus auf einem Feiertag befindet
            base.OnMouseMove(e);
            if (e.Location != this.lastMouseLocation)
            {
                this.lastMouseLocation = e.Location;
                if (this.holidays != null)
                {
                    DateTime date = this.HitTest(e.X, e.Y).Time;
                    foreach (GermanSpecialDay holiday in this.holidays.Values)
                    {
                        if (holiday.Date.Date == date.Date)
                        {
                            this.toolTip.SetToolTip(this, holiday.Name);
                            return;
                        }
                    }
                    this.toolTip.SetToolTip(this, null);
                }
            }
        }

        ///*      protected override void OnPaint(PaintEventArgs e)
        //      {
        //          base.OnPaint(e);
        //          SelectionRange calendarRange = GetDisplayRange(false);
        //          if (this.CalendarDimensions.Equals(new Size(1, 1)) && (calendarRange.End - calendarRange.Start) < TimeSpan.FromDays(50))
        //          { //nur wenn Einzeltage angezeigt werden - nicht bei Monats oder Jahresauswahl!
        //              Graphics graphics = e.Graphics;
        //              int dayBoxWidth = ShowWeekNumbers ? 24 : 24;
        //              int dayBoxHeight = 15;

        //              List<DateTime> visibleWarningDates = new List<DateTime>();
        //              foreach (DateTime date in WarningDates) { if (date >= calendarRange.Start && date <= calendarRange.End) { visibleWarningDates.Add(date); } }

        //              List<DateTime> visibleBoldDates = new List<DateTime>();
        //              foreach (DateTime date in BoldedDates) { if (date >= calendarRange.Start && date <= calendarRange.End) { visibleBoldDates.Add(date); } }

        //              int firstWeekPosition = 50;
        //              using (Brush warningBrush = new SolidBrush(Color.LightSkyBlue)) // Color.FromArgb(255, Color.FromArgb(255, 240, 240))
        //              {
        //                  foreach (DateTime visDate in visibleWarningDates)
        //                  {
        //                      int row = 0, col = 0;
        //                      TimeSpan span = visDate.Subtract(calendarRange.Start);
        //                      row = span.Days / 7;
        //                      col = span.Days % 7;
        //                      Rectangle fillRect = new Rectangle((col + (ShowWeekNumbers ? 1 : 0)) * dayBoxWidth + 2, firstWeekPosition + row * dayBoxHeight + 1, dayBoxWidth - 3, dayBoxHeight - 3);
        //                      graphics.FillRectangle(warningBrush, fillRect);
        //                      fillRect.Width -= 1; // wg. TextFormatFlags.Right
        //                      bool makeDateBolded = false; // Check if the date is in the bolded dates array 
        //                      foreach (DateTime boldDate in BoldedDates) { if (boldDate == visDate) { makeDateBolded = true; } }
        //                      using (Font textFont = new Font(Font, (makeDateBolded ? FontStyle.Bold : FontStyle.Regular)))
        //                      {
        //                          TextRenderer.DrawText(graphics, visDate.Day.ToString(), textFont, fillRect, Color.DarkBlue, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        //                      }
        //                  }
        //              }
        //              using (Brush boldedBrush = new SolidBrush(Color.Gold))
        //              {
        //                  foreach (DateTime visDate in visibleBoldDates)
        //                  {
        //                      int row = 0, col = 0;
        //                      TimeSpan span = visDate.Subtract(calendarRange.Start);
        //                      row = span.Days / 7;
        //                      col = span.Days % 7;
        //                      Rectangle fillRect = new Rectangle((col + (ShowWeekNumbers ? 1 : 0)) * dayBoxWidth + 2, firstWeekPosition + row * dayBoxHeight + 1, dayBoxWidth - 3, dayBoxHeight - 3);
        //                      graphics.FillRectangle(boldedBrush, fillRect);
        //                      fillRect.Width -= 1; // wg. TextFormatFlags.Right - aber nicht wenn B
        //                      using (Font textFont = new Font(Font, FontStyle.Bold))
        //                      {
        //                          TextRenderer.DrawText(graphics, visDate.Day.ToString(), textFont, fillRect, Color.DarkRed, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        //                      }
        //                  }
        //              }
        //          }
        //      } */
    }
}

//Basically I wanted to be able to identify where in the MonthCalendar a certain date was appearing so that I could draw a red square on it.
//For this I needed to figure out the dimensions and appearance of the control.I started with trying to figure out where in the control the 
//actual dates were being drawn. I didn’t come up with a good way of doing this, and ended up having to use a method called HitTest to try and
//figure out which area is which in the control. This was a really ugly approach, but I didn’t have time to look for another solution.

//I went about it like this:

//int firstWeekPosition = 0;
//int lastWeekPosition = Height;

//while ((HitTest(25, firstWeekPosition).HitArea != HitArea.PrevMonthDate &&
//  HitTest(25, firstWeekPosition).HitArea != HitArea.Date) && firstWeekPosition < Height)
//{
//    firstWeekPosition++;
//}

//while ((HitTest(25, lastWeekPosition).HitArea != HitArea.NextMonthDate && 
//  HitTest(25, firstWeekPosition).HitArea != HitArea.Date) && firstWeekPosition >= 0)
//{
//    lastWeekPosition--;
//}

//What basically is going on here is that I traverse the control first from the top and then from the bottom in a straight line looking for the
//Date-area of the control. I arbitrarily chose to do this at 25 pixels into the control from the left, simply because it was likely that I would 
//be sure to come across the date-area when looking there. I will not be winning any beauty contests with this code.

//When I’d done that I needed to calculate the area that is allocated for each date. I did that like this:

//int dayBoxWidth = 0; 
//int dayBoxHeight = 0;

//dayBoxWidth = Width / (ShowWeekNumbers ? 8 : 7);
//dayBoxHeight = (int)(((float)(lastWeekPosition - firstWeekPosition)) / 6.0f);

//This gave me everything I needed for finding out where to paint.