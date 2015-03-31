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
    /// <summary>
    /// string扩展类
    /// </summary>
    public static class StringExpansion
    {
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
    }
}
