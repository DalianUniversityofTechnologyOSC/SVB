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
            int idle = 0;
            int busy = 0;
            sb.Append("[");
            foreach (SportsVenueBookingCommon.Models.Duration d in durations)
            {
                foreach (SportsVenueBookingCommon.Models.Space s in spaces)
                {
                    ts.Clear();
                    idle = 0;
                    busy = 0;
                    ts.Add(d.duration_StartTime, d.duration_EndTime);
                    if (duration_reseration.Where(k => (ts.IsContain(k.reservation_StartTime) || ts.IsContain(k.reservation_EndTime)) && k.Snooker.Space.space_Id == s.space_Id).Count() >= 1)
                    {
                        busy++;
                    }
                    else
                    {
                        idle++;
                    }

                    sb.Append("{\"id\":\"" + d.duration_Id + "\",\"time\":\"" + d.duration_Name + "\",\"type\",\"" + s.space_Name + "\",\"idle\":\"" + idle + "\",\"appointment\":\"" + busy + "\",\"isAllIdle\":\"" + busy + "\",\"seach\":\"-1\",\"remarks\":\"" + idle + "\"},");
                }
            }
            return sb.ToString().Substring(-1, 1) + "]";
        }

        public List<SportsVenueBookingCommon.Models.Reservation> GetDurationAppointInfo(string durationId, string startDate, string endDate)
        {
            TimeSlot ts = new Duration().GetDurationTimeSlot(durationId, startDate, endDate);
            return base.Search(d => d.reservation_IsDel == false).Where(d => ts.IsContain(d.reservation_StartTime) || ts.IsContain(d.reservation_EndTime)).ToList();
        }
    }
}
