//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportsVenueBookingCommon.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Snooker
    {
        public Snooker()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public long snooker_Id { get; set; }
        public int snooker_Space { get; set; }
        public int snooker_Number { get; set; }
        public long snooker_User { get; set; }
        public double snooker_Price { get; set; }
        public bool snooker_IsDel { get; set; }
    
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual Space Space { get; set; }
        public virtual User User { get; set; }
    }
}