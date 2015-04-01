using SportsVenueBookingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingBLL
{
    #region Duration逻辑操作类+public class Duration : BaseBLL<SportsVenueBookingCommon.Models.Duration>
    /// <summary>
    /// Duration逻辑操作类
    /// </summary>
    public class Duration : BaseBLL<SportsVenueBookingCommon.Models.Duration>
    {
        #region 根据课程时间Id获取课程信息+public List<SportsVenueBookingCommon.Models.Duration> GetDurationInfo(string tId)
        /// <summary>
        /// 根据课程时间Id获取课程信息
        /// </summary>
        /// <param name="tId">课程Id</param>
        /// <returns>查询到的课程信息</returns>
        public List<SportsVenueBookingCommon.Models.Duration> GetDurationInfo(string tId)
        {
            if (tId == "0")
            {
                return base.Search(d => d.duration_IsDel == false);
            }
            else
            {
                long id = Convert.ToInt64(tId);
                return base.Search(d => d.duration_Id == id && d.duration_IsDel == false);
            }
        }
        #endregion

        #region 获取某一课程的时间段+public TimeSlot GetDurationTimeSlot(string tId, string startDate, string endDate)
        /// <summary>
        /// 获取某一课程的时间段
        /// </summary>
        /// <param name="tId">课程Id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>课程时间段</returns>
        public TimeSlot GetDurationTimeSlot(string tId, string startDate, string endDate)
        {
            List<SportsVenueBookingCommon.Models.Duration> durations = this.GetDurationInfo(tId);
            TimeSlot ts = new TimeSlot();
            ts.IsGlobalDate = true;
            ts.StartDate = startDate.ToDateTime();
            ts.EndDate = endDate.ToDateTime();
            foreach (SportsVenueBookingCommon.Models.Duration d in durations)
            {
                ts.Add(d.duration_StartTime, d.duration_EndTime);
            }
            return ts;
        }
        #endregion

        #region 获取所有课程时间+public List<SportsVenueBookingCommon.Models.Duration> GetDurationList()
        /// <summary>
        /// 获取所有课程时间
        /// </summary>
        /// <returns>课程时间集合</returns>
        public List<SportsVenueBookingCommon.Models.Duration> GetAllDuration()
        {
            return base.Search(d => d.duration_IsDel == false);
        }
        #endregion
    }
    #endregion
}
