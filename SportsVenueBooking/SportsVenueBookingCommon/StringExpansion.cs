/*
 * 作者：李贵发
 * 时间：2015-03-31
 * 功能：扩展string类型
 * 文件：StringExpansion.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingCommon
{
    #region string扩展类+public static class StringExpansion
    /// <summary>
    /// string扩展类
    /// </summary>
    public static class StringExpansion
    {
        #region 去除string类型中指定的字符+public static string RemoveChar(this string str, char ch)
        /// <summary>
        /// 去除string类型中指定的字符
        /// </summary>
        /// <param name="str">要去除字符的字符串</param>
        /// <param name="ch">要去除的字符</param>
        /// <returns>去除字符后的新字符串</returns>
        public static string RemoveChar(this string str, char ch)
        {
            string[] strArr = str.Split(ch);
            string newStr = String.Empty;
            foreach (string s in strArr)
            {
                newStr += s;
            }
            return newStr;
        }
        #endregion

        #region 将string类型转换成DateTime类型+public static DateTime ToDateTime(this string str)
        /// <summary>
        /// 将string类型转换成DateTime类型
        /// </summary>
        /// <param name="str">要转换的string类型</param>
        /// <returns>转换后的DateTime对象</returns>
        public static DateTime ToDateTime(this string str)
        {
            string[] strArr = str.Split('/');
            return new DateTime(Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr[1]), Convert.ToInt32(strArr[2]));
        }
        #endregion

        public static DayOfWeek ToDayOfWeek(this string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "0": return DayOfWeek.Sunday;
                case "1": return DayOfWeek.Monday;
                case "2": return DayOfWeek.Tuesday;
                case "3": return DayOfWeek.Wednesday;
                case "4": return DayOfWeek.Thursday;
                case "5": return DayOfWeek.Friday;
                case "6": return DayOfWeek.Saturday;
                default: return new DayOfWeek();
            }
        }
    }
    #endregion
}
