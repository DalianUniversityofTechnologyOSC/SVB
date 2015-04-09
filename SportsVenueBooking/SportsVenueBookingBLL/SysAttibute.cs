using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsVenueBookingCommon;

namespace SportsVenueBookingBLL
{
    public class SysAttibute
    {
        SportsVenueBookingDAL.SysAttibute db = new SportsVenueBookingDAL.SysAttibute();

        /// <summary>
        /// 获取当前学期的上课周数
        /// </summary>
        /// <returns></returns>
        public int GetSemesterWeekNumber()
        {
            DateTime start = db.Search(d => d.start).ToDateTime();
            DateTime end = db.Search(d => d.end).ToDateTime();
            return (int)Math.Ceiling((double)((end.DayOfYear - start.DayOfYear) / 7));
        }
    }
}
