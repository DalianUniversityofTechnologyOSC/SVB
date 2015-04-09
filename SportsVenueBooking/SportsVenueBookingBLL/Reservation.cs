using SportsVenueBookingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingBLL
{
    /// <summary>
    /// 预约表逻辑方法类
    /// </summary>
    public class Reservation : BaseBLL<SportsVenueBookingCommon.Models.Reservation>
    {
        /// <summary>
        /// 查询一定时段的预约情况
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="conditions">是否查询全部时间空闲</param>
        /// <param name="type">场地类型</param>
        /// <param name="time">课程时间</param>
        /// <param name="dayOfWeek">日期星期</param>
        /// <returns>预约情况Json数据</returns>
        public string GetAppointmentInfo(string startDate, string endDate, string conditions, string type, string time, string dayOfWeek)
        {
            List<SportsVenueBookingCommon.Models.Reservation> duration_reseration = this.GetDurationAppointInfo(time, startDate, endDate, dayOfWeek);
            List<SportsVenueBookingCommon.Models.Duration> durations = new Duration().GetDurationInfo(time);
            List<SportsVenueBookingCommon.Models.Space> spaces = new Space().GetSpaceList(type);
            StringBuilder sb = new StringBuilder();
            TimeSlot ts = new TimeSlot();
            ts.IsGlobalDate = true;
            ts.StartDate = startDate.ToDateTime();
            ts.EndDate = endDate.ToDateTime();
            int idle = 0;
            int busy = 0;
            int common = dayOfWeek == "-1" ? (endDate.ToDateTime() - startDate.ToDateTime()).Days + 1 : ((endDate.ToDateTime() - startDate.ToDateTime()).Days + 1) / 7;
            sb.Append("[");
            foreach (SportsVenueBookingCommon.Models.Duration d in durations)
            {
                foreach (SportsVenueBookingCommon.Models.Space s in spaces)
                {
                    ts.Clear();
                    idle = 0;
                    busy = 0;
                    ts.Add(d.duration_StartTime, d.duration_EndTime);

                    busy = duration_reseration.Where(k => (ts.IsContain(k.reservation_StartTime) || ts.IsContain(k.reservation_EndTime)) && k.Snooker.Space.space_Id == s.space_Id).Count();
                    idle = common - busy;
                    if ((conditions == "1" && busy == 0) || conditions == "0")
                    {
                        sb.Append("{\"id\":\"" + d.duration_Id + "\",\"time\":\"" + d.duration_Name + "\",\"type\":\"" + s.space_Name + "\",\"idle\":\"" + idle + "\",\"appointment\":\"" + busy + "\",\"isAllIdle\":\"" + busy + "\",\"seach\":\"" + (busy == 0 ? "0" : d.duration_Id.ToString() + "/" + s.space_Id) + "\",\"remarks\":\"" + (idle == 0 ? "-1" : d.duration_Id.ToString() + "/" + s.space_Id) + "\"},");
                    }
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1) + "]";
        }

        /// <summary>
        /// 获取预约情况
        /// </summary>
        /// <param name="durationId">课程时间</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="dayOfWeek">日期星期</param>
        /// <returns>预约情况</returns>
        private List<SportsVenueBookingCommon.Models.Reservation> GetDurationAppointInfo(string durationId, string startDate, string endDate, string dayOfWeek)
        {
            TimeSlot ts = new Duration().GetDurationTimeSlot(durationId, startDate, endDate);
            return base.Search(d => d.reservation_IsDel == false).Where(d => (ts.IsContain(d.reservation_StartTime) || ts.IsContain(d.reservation_EndTime)) && (d.reservation_StartTime.DayOfWeek.ToInt() == Convert.ToInt32(dayOfWeek) || dayOfWeek == "-1")).ToList();
        }

        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="duration">课程时间</param>
        /// <param name="space">场地</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="userId">预约人</param>
        /// <param name="dayOfWeek">预约星期数</param>
        /// <returns>预约结果状态</returns>
        public StatusAttribute ReservationIn(string duration, string space, string start, string end, string userId, string dayOfWeek)
        {
            StatusAttribute res = new StatusAttribute();
            List<SportsVenueBookingCommon.Models.Duration> d = new Duration().GetDurationInfo(duration);
            if (d.Count == 1)
            {
                int start_int = Convert.ToInt32(start.RemoveChar('/'));
                int end_int = Convert.ToInt32(end.RemoveChar('/'));
                int today = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                if (start_int >= today)
                {
                    if (end_int >= start_int)
                    {
                        DateTime startDate = start.ToDateTime();
                        DateTime endDate = end.ToDateTime();
                        List<DateTime> timeGroup = startDate.ToTimeGroupOfTimeSpan(endDate, dayOfWeek.ToDayOfWeek());
                        foreach (DateTime dt in timeGroup)
                        {
                            SportsVenueBookingCommon.Models.Reservation r = new SportsVenueBookingCommon.Models.Reservation()
                            {
                                reservation_Id = 1,
                                reservation_IsBilling = false,
                                reservation_IsDel = false,
                                reservation_User = Convert.ToInt64(userId),
                                reservation_StartTime = d[0].duration_StartTime.ToDateTime(dt),
                                reservation_EndTime = d[0].duration_EndTime.ToDateTime(dt),
                                reservation_Snooker = Convert.ToInt32(space)
                            };
                            base.Add(r);
                        }
                        res.status = true;
                        res.message = "预约成功！";
                    }
                    else
                    {
                        res.status = false;
                        res.message = "预约失败！结束时间必须大于等于开始时间...";
                    }
                }
                else
                {
                    res.status = false;
                    res.message = "预约失败！开始时间必须大于等于当前时间...";
                }
            }
            else
            {
                res.status = false;
                res.message = "预约失败！课程时间不存在...";
            }
            return res;
        }

        /// <summary>
        /// 查询已预约的详细情况
        /// </summary>
        /// <param name="duration">课程时间</param>
        /// <param name="space">场地类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="dayOfWeek">星期</param>
        /// <returns>已预约的情况</returns>
        public string SearchReservationJson(string duration, string space, string startDate, string endDate, string dayOfWeek)
        {
            List<SportsVenueBookingCommon.Models.Duration> durations = new Duration().GetDurationInfo(duration);
            List<SportsVenueBookingCommon.Models.Reservation> duration_reseration = this.GetDurationAppointInfo(duration, startDate, endDate, dayOfWeek).Where(d => d.Snooker.Space.space_Id == Convert.ToInt32(space)).ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (SportsVenueBookingCommon.Models.Reservation r in duration_reseration)
            {
                sb.Append("{\"date\":\"" + r.reservation_StartTime.Date + "\",\"dayOfWeek\":\"" + r.reservation_StartTime.DayOfWeek.ToInt() + "\",\"start\":\"" + r.reservation_StartTime.TimeOfDay + "\",\"end\":\"" + r.reservation_EndTime.TimeOfDay + "\",\"user\":\"" + r.User.user_Name + "\",\"user_type\":\"" + r.User.user_Type + "\"},");
            }
            return sb.ToString().Substring(0, sb.Length - 1) + "]";
        }

        public string GetMyReservationData(long id)
        {
            List<TeacherReservation> teacherReservations = new List<TeacherReservation>();
            List<SportsVenueBookingCommon.Models.Reservation> reservations = base.Search(d => d.reservation_IsDel == false && d.User.user_Id == id).Where(d => d.reservation_StartTime.CompareTo(DateTime.Now) >= 0).OrderBy(d => d.reservation_StartTime).ToList();
            //List<System.Linq.Lookup<System.DayOfWeek, SportsVenueBookingCommon.Models.Reservation>.Grouping> group = reservations.GroupBy(d => d.reservation_StartTime.DayOfWeek).ToList();
            
            return "";
        }

        class TeacherReservation
        {
            public DateTime start { get; set; }

            public DateTime end { get; set; }

            public int day { get; set; }

            public int duration { get; set; }
        }
    }
}
