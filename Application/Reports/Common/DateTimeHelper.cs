using System;
using System.Collections.Generic;

namespace Money2.Application.Reports
{
    internal static class DateTimeHelper
    {
        public static readonly List<String> MonthNames = new List<String> {
            "Январь", "Февраль",
            "Март", "Апрель", "Май",
            "Июнь", "Июль", "Август",
            "Сентябрь", "Октябрь", "Ноябрь",
            "Декабрь" };

        public static List<Int32> GetLastYears( Int32 count )
        {
            var years = new List<Int32>();

            var year = DateTime.Now.Year;
            for ( var i = 0; i < count; i++ )
            {
                years.Add( year - i );
            }

            return years;
        }

        public static List<DateTime> GetLastMonthYears( Int32 count )
        {
            var result = new List<DateTime>();

            var date = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 );
            for ( var i = 0; i < count; i++ )
            {
                result.Add( date.AddMonths( -i ) );
            }

            return result;
        }

        public static List<Month> GetLastMonths( Int32 count )
        {
            var result = new List<Month>();

            var date = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 );
            for ( var i = 0; i < count; i++ )
            {
                var month = new Month()
                {
                    Name = MonthNames[ date.Month - 1 ],
                    StartDate = date,
                    EndDate = new DateTime( date.Year, date.Month, DateTime.DaysInMonth( date.Year, date.Month ) )
                };

                result.Add( month );

                date = date.AddMonths( -1 );
            }

            return result;
        }
    }
}
