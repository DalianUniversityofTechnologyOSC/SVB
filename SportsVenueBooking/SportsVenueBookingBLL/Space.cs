using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingBLL
{
    public class Space : BaseBLL<SportsVenueBookingCommon.Models.Space>
    {
        public List<SportsVenueBookingCommon.Models.Space> GetSpaceList(string type)
        {
            if (type == "0")
            {
                return base.Search(d => d.space_IsDel == false);
            }
            else
            {
                long spaceId = Convert.ToInt64(type);
                return base.Search(d => d.space_IsDel == false && d.space_Id == spaceId);
            }
        }
    }
}
