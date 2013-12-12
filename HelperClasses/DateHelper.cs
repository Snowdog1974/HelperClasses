using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public class DateClass
    {

        public DateClass()
        {

        }
        public static List<DateTime> getWeekDates(DateTime startDate)
        {
            DateTime endDate = startDate.AddDays(7);
            List<DateTime> result = new List<DateTime>();
            TimeSpan ts = endDate.Subtract(startDate);
            for (DateTime tempDt = startDate; tempDt < endDate; tempDt = tempDt.AddDays(1))
            {
                if (tempDt.DayOfWeek != DayOfWeek.Saturday && tempDt.DayOfWeek != DayOfWeek.Sunday)
                {
                    result.Add(tempDt);

                }
            }
            return result;
        }

        public static List<WeekDates> getWeekDates(DateTime startDate, int NoWeeks)
        {
            int days = NoWeeks * 7;
            DateTime enddate = startDate.AddDays(days);

            List<WeekDates> result = new List<WeekDates>();


            for (DateTime tempdate = startDate; tempdate < enddate; tempdate = tempdate.AddDays(7))
            {
                WeekDates wdate = new WeekDates();
                wdate.Weekno = GetWeekNumber(tempdate);

                wdate.Fromdate = FirstDateOfWeek(tempdate.Year, GetWeekNumber(tempdate), CalendarWeekRule.FirstDay);
                wdate.Todate = wdate.Fromdate.AddDays(6);
                result.Add(wdate);
            }

            return result;
        }



        public static int getDateSpanwWeekends(DateTime startDate, DateTime endDate)
        {
            TimeSpan ts = endDate.Subtract(startDate);
            int weekends = 0;
            for (DateTime tempDt = startDate; tempDt < endDate; tempDt = tempDt.AddDays(1))
            {
                if (tempDt.DayOfWeek == DayOfWeek.Saturday || tempDt.DayOfWeek == DayOfWeek.Sunday)
                    weekends++;
            }
            ts = ts.Subtract(new TimeSpan(weekends, 0, 0, 0));
            return ts.Days;
        }

        public static DateTime FirstDateOfWeek(int year, int weekNum, CalendarWeekRule rule)
        {
            Debug.Assert(weekNum >= 1);
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            Debug.Assert(firstMonday.DayOfWeek == DayOfWeek.Monday);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, rule, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            DateTime result = firstMonday.AddDays(weekNum * 7);
            return result;
        }

        public static string GetYearWeekNoDayNo(DateTime dt)
        {
            return dt.Year + "-" + GetWeekNumber(dt) + "-" + GetDayNoInWeek(dt);

        }


        public static int GetDayNoInWeek(DateTime dtPassed)
        {
            int day = 0;


            if (DayOfWeek.Monday == dtPassed.DayOfWeek)
            {
                day = 1;
            }
            else if (DayOfWeek.Tuesday == dtPassed.DayOfWeek)
            {
                day = 2;
            }
            else if (DayOfWeek.Wednesday == dtPassed.DayOfWeek)
            {
                day = 3;
            }
            else if (DayOfWeek.Thursday == dtPassed.DayOfWeek)
            {
                day = 4;
            }
            else if (DayOfWeek.Friday == dtPassed.DayOfWeek)
            {
                day = 5;
            }
            else if (DayOfWeek.Saturday == dtPassed.DayOfWeek)
            {
                day = 6;
            }
            else if (DayOfWeek.Sunday == dtPassed.DayOfWeek)
            {
                day = 7;
            }

            return day;
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {


            CultureInfo ciCurr = CultureInfo.CurrentCulture;


            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);


            return weekNum;


        }





    }
    /// <summary>
    /// Class created to get first and last day of week in occupy
    /// </summary>
    public class WeekDates
    {

        private int weekno;
        public int Weekno
        {
            get { return weekno; }
            set
            {
                weekno = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Weekno"));
            }

        }

        private DateTime fromdate;
        public DateTime Fromdate
        {
            get { return fromdate; }
            set
            {
                fromdate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Fromdate"));
            }

        }

        private DateTime todate;
        public DateTime Todate
        {
            get { return todate; }
            set
            {
                todate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Todate"));
            }

        }

        public WeekDates()
        {

        }

        public WeekDates(DateTime fromdate, DateTime todate, int weekno)
        {
            Fromdate = fromdate;
            Todate = todate;
            Weekno = weekno;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


    }
}
