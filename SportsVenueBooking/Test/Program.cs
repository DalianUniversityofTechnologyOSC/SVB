using SportsVenueBookingCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDatabase<SportsVenueBookingCommon.Models.sysAttibute> xd = new XmlDatabase<SportsVenueBookingCommon.Models.sysAttibute>();
            Console.WriteLine(xd.Select(d => d.start));
            Console.Read();
        }
    }
}
