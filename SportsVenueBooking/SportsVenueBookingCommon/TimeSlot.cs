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
    /// <summary>
    /// 时间段，表示一组时间开始到结束
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// 时间组，用于比较的时间聚合
        /// </summary>
        List<DateTime[]> timeSlot = new List<DateTime[]>();

        /// <summary>
        /// 是否全部时间组使用同一日期
        /// </summary>
        public bool IsGlobalDate = false;

        /// <summary>
        /// 全局日期开始时间，IsGloal=true有效
        /// </summary>
        DateTime startDate = new DateTime();

        /// <summary>
        /// 开始日期set,get方法
        /// </summary>
        public DateTime StartDate
        {
            set
            {
                if (endDate != null)
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



        /// <summary>
        /// 全局日期结束时间，IsGloal=true有效
        /// </summary>
        DateTime endDate = new DateTime();

        /// <summary>
        /// 结束日期get,set方法
        /// </summary>
        public DateTime EndDate
        {
            set
            {
                if (endDate != null)
                {
                    if (value.CompareTo(startDate) <= 0)
                    {
                        this.endDate = this.startDate;
                    }
                    else
                    {
                        startDate = value;
                    }
                }
                else
                {
                    startDate = value;
                }
            }
            get
            {
                return this.endDate;
            }
        }

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
                for (int i = 0; tempDate.CompareTo(endDate) >= 0; i++)
                {
                    DateTime d_s = new DateTime(Convert.ToInt64(new DateTime(1970, 1, 1, 8, 0, 0).Ticks) + Convert.ToInt64(tempDate.ToString().RemoveChar(':').RemoveChar('/')) + Convert.ToInt64(startTime.ToString().RemoveChar(':').RemoveChar('/')));
                    DateTime d_e = new DateTime(Convert.ToInt64(new DateTime(1970, 1, 1, 8, 0, 0).Ticks) + Convert.ToInt64(tempDate.ToString().RemoveChar(':').RemoveChar('/')) + Convert.ToInt64(endTime.ToString().RemoveChar(':').RemoveChar('/')));
                    DateTime[] d = new DateTime[2] { d_s, d_e };
                    timeSlot.Add(d);
                }
            }
            else
            {
                DateTime[] dateTime = new DateTime[2] { new DateTime(Convert.ToInt64(startTime.ToString().RemoveChar(':'))), new DateTime(Convert.ToInt64(startTime.ToString().RemoveChar(':'))) };
                timeSlot.Add(dateTime);
            }
        }

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

        /// <summary>
        /// 清空时间组
        /// </summary>
        public void Clear()
        {
            timeSlot.Clear();
        }
    }
}
