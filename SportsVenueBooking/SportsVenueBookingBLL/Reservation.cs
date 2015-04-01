using SportsVenueBookingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingBLL
{
    public class Reservation : BaseBLL<SportsVenueBookingCommon.Models.Reservation>
    {
        public string GetAppointmentInfo(string startDate, string endDate, string conditions, string type, string time)
        {
            List<SportsVenueBookingCommon.Models.Reservation> duration_reseration = this.GetDurationAppointInfo(time, startDate, endDate);
            List<SportsVenueBookingCommon.Models.Duration> durations = new Duration().GetDurationInfo(time);
            List<SportsVenueBookingCommon.Models.Space> spaces = new Space().GetSpaceList(type);
            StringBuilder sb = new StringBuilder();
            TimeSlot ts = new TimeSlot();
            ts.IsGlobalDate = true;
            ts.StartDate = startDate.ToDateTime();
            ts.EndDate = endDate.ToDateTime();
            int idle = 0;
            int busy = 0;
            int common = (endDate.ToDateTime() - startDate.ToDateTime()).Days + 1;
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
                        sb.Append("{\"id\":\"" + d.duration_Id + "\",\"time\":\"" + d.duration_Name + "\",\"type\":\"" + s.space_Name + "\",\"idle\":\"" + idle + "\",\"appointment\":\"" + busy + "\",\"isAllIdle\":\"" + idle + "\",\"seach\":\"" + idle + "\",\"remarks\":\"" + (idle == 0 ? "-1" : d.duration_Id.ToString() + "/" + s.space_Id) + "\"},");
                    }
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1) + "]";
        }

        public List<SportsVenueBookingCommon.Models.Reservation> GetDurationAppointInfo(string durationId, string startDate, string endDate)
        {
            TimeSlot ts = new Duration().GetDurationTimeSlot(durationId, startDate, endDate);
            return base.Search(d => d.reservation_IsDel == false).Where(d => ts.IsContain(d.reservation_StartTime) || ts.IsContain(d.reservation_EndTime)).ToList();
        }

        public StatusAttribute ReservationIn(string duration, string space, string start, string end, string userId)
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
                        for (; startDate.CompareTo(endDate) <= 0; startDate = startDate.AddDays(1))
                        {
                            SportsVenueBookingCommon.Models.Reservation r = new SportsVenueBookingCommon.Models.Reservation()
                            {
                                reservation_Id = 1,
                                reservation_IsBilling = false,
                                reservation_IsDel = false,
                                reservation_User = Convert.ToInt64(userId),
                                reservation_StartTime = d[0].duration_StartTime.ToDateTime(startDate),
                                reservation_EndTime = d[0].duration_EndTime.ToDateTime(startDate),
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
    }
}
