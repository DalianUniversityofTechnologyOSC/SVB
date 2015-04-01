/*
 * 作者：李贵发
 * 时间：2015-03-31
 * 功能：判断某一时间是否在某一组时间段中
 * 文件：TimeSlot.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingCommon
{
    #region 时间段，表示一组时间开始到结束+public class TimeSlot
    /// <summary>
    /// 时间段，表示一组时间开始到结束
    /// </summary>
    public class TimeSlot
    {
        #region 时间组，用于比较的时间聚合+List<DateTime[]> timeSlot = new List<DateTime[]>();
        /// <summary>
        /// 时间组，用于比较的时间聚合
        /// </summary>
        List<DateTime[]> timeSlot = new List<DateTime[]>();
        #endregion

        #region 是否全部时间组使用同一日期+public bool IsGlobalDate = false;
        /// <summary>
        /// 是否全部时间组使用同一日期
        /// </summary>
        public bool IsGlobalDate = false;
        #endregion

        #region 全局日期开始时间，IsGloal=true有效+DateTime startDate = new DateTime();
        /// <summary>
        /// 全局日期开始时间，IsGloal=true有效
        /// </summary>
        DateTime startDate = new DateTime();
        #endregion

        #region 开始日期set,get方法+public DateTime StartDate
        /// <summary>
        /// 开始日期set,get方法
        /// </summary>
        public DateTime StartDate
        {
            set
            {
                if (value.CompareTo(endDate) >= 0)
                {
                    this.startDate = this.endDate;
                }
                else
                {
                    startDate = value;
                }
            }
            get
            {
                return startDate;
            }
        }
        #endregion

        #region 全局日期结束时间，IsGloal=true有效+DateTime endDate = new DateTime();
        /// <summary>
        /// 全局日期结束时间，IsGloal=true有效
        /// </summary>
        DateTime endDate = new DateTime(9999, 9, 9);
        #endregion

        #region 结束日期get,set方法+public DateTime EndDate
        /// <summary>
        /// 结束日期get,set方法
        /// </summary>
        public DateTime EndDate
        {
            set
            {
                if (value.CompareTo(startDate) <= 0)
                {
                    this.endDate = this.startDate;
                }
                else
                {
                    endDate = value;
                }
            }
            get
            {
                return this.endDate;
            }
        }
        #endregion

        #region 添加时间组成员+public void Add(TimeSpan startTime, TimeSpan endTime)
        /// <summary>
        /// 添加时间组成员
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public void Add(TimeSpan startTime, TimeSpan endTime)
        {
            if (IsGlobalDate)
            {
                DateTime tempDate = startDate;
                for (; tempDate.CompareTo(endDate) <= 0; tempDate = tempDate.AddDays(1))
                {
                    DateTime[] d = new DateTime[2] { startTime.ToDateTime(tempDate), endTime.ToDateTime(tempDate) };
                    timeSlot.Add(d);
                    ;
                }
            }
            else
            {
                DateTime[] dateTime = new DateTime[2] { new DateTime(Convert.ToInt64(startTime.ToString().RemoveChar(':'))), new DateTime(Convert.ToInt64(startTime.ToString().RemoveChar(':'))) };
                timeSlot.Add(dateTime);
            }
        }
        #endregion

        #region 判断某一时间是否包含于某一时间组中+public bool IsContain(DateTime time)
        /// <summary>
        /// 判断某一时间是否包含于某一时间组中
        /// </summary>
        /// <param name="time">要判断的时间</param>
        /// <returns>包含：true，不包含：false</returns>
        public bool IsContain(DateTime time)
        {
            foreach (DateTime[] dateTime in this.timeSlot)
            {
                if (time.CompareTo(dateTime[0]) >= 0 && time.CompareTo(dateTime[1]) <= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 清空时间组+public void Clear()
        /// <summary>
        /// 清空时间组
        /// </summary>
        public void Clear()
        {
            timeSlot.Clear();
        }
        #endregion
    }
    #endregion
}
