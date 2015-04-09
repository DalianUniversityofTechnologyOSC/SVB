using SportsVenueBookingCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SportsVenueBookingDAL
{
    public class SysAttibute
    {
        XmlDatabase<SportsVenueBookingCommon.Models.sysAttibute> xd = new XmlDatabase<SportsVenueBookingCommon.Models.sysAttibute>();

        public string Search(Expression<Func<SportsVenueBookingCommon.Models.sysAttibute, string>> s)
        {
            return xd.Select(s);
        }
    }
}
