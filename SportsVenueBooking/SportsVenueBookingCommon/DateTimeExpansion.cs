/*
 * 时间：2015-04-04
 * 作者：李贵发
 * 功能：DateTime扩展
 * 文件：DateTimeExpansion.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingCommon
{
    /// <summary>
    /// DataTime扩展类
    /// </summary>
    public static class DateTimeExpansion
    {
        /// <summary>
        /// 从给定的时间段内提取给定星期的日期组
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="dayOfWeek">日期星期</param>
        /// <returns>提取到的日期组</returns>
        public static List<DateTime> ToTimeGroupOfTimeSpan(this DateTime start, DateTime end, DayOfWeek dayOfWeek)
        {
            List<DateTime> timeGroup = new List<DateTime>();
            int s = start.DayOfWeek.ToInt() == 0 ? 7 : start.DayOfWeek.ToInt();
            int d = dayOfWeek.ToInt() == 0 ? 7 : dayOfWeek.ToInt();
            int dayDifference = d - s;
            start = start.AddDays(dayDifference);
            for (; start.CompareTo(end) <= 0; start = start.AddDays(7))
            {
                timeGroup.Add(start);
            }
            return timeGroup;
        }
    }
}
