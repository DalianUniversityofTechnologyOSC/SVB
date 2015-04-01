/*
 * 作者：李贵发
 * 时间：2015-04-01
 * 功能：扩展TimeSpan类
 * 文件：TimeSpanExpansion.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingCommon
{
    #region 扩展TimeSpan类+public static class TimeSpanExpansion
    /// <summary>
    /// 扩展TimeSpan类
    /// </summary>
    public static class TimeSpanExpansion
    {
        #region 将TimeSpan与给定的日期组合成新的日期时间+public DateTime ToDateTime(this TimeSpan ts, DateTime date)
        /// <summary>
        /// 将TimeSpan与给定的日期组合成新的日期时间
        /// </summary>
        /// <param name="ts">要转换的时间</param>
        /// <param name="date">要组合的日期</param>
        /// <returns>组合成的日期</returns>
        public static DateTime ToDateTime(this TimeSpan ts, DateTime date)
        {
            string[] dateArr = date.ToString().Split(' ')[0].Split('/');
            string[] tsArr = ts.ToString().Split(':');
            return new DateTime(Convert.ToInt32(dateArr[0]), Convert.ToInt32(dateArr[1]), Convert.ToInt32(dateArr[2]), Convert.ToInt32(tsArr[0]), Convert.ToInt32(tsArr[1]), Convert.ToInt32(tsArr[2]));
        }
        #endregion
    }
    #endregion
}
