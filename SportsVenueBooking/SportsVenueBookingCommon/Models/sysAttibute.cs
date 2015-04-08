using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SportsVenueBookingCommon.Models
{
    public class sysAttibute
    {
        public XDocument sysConfig;
        public sysAttibute()
        {
            sysConfig = XDocument.Load("");
        }
    }
}
