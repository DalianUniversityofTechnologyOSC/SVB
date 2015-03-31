using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsVenueBookingBLL
{
    public class User : BaseBLL<SportsVenueBookingCommon.Models.User>
    {
        public string GetUserInfo(long tId)
        {
            return this.UserToJson(base.Search(d => d.user_Id == tId && d.user_IsDel == false).First());
        }

        public string UserToJson(SportsVenueBookingCommon.Models.User user)
        {
            return "{\"total\":2,\"rows\":[{\"name\":\"用户编号\",\"value\":\"" + user.user_UserNumber + "\"},{\"name\":\"用户类别\",\"value\":\"" + (user.user_Type == 1 ? "学生" : user.user_Type == 2 ? "教师" : "校外人员") + "\"},{\"name\":\"用户姓名\",\"value\":\"" + user.user_Name + "\"},{\"name\":\"用户班级\",\"value\":\"" + user.user_Class + "\"},{\"name\":\"用户状态\",\"value\":\"" + (user.user_Status == true ? "锁定" : "正常") + "\"},{\"name\":\"备注\",\"value\":\"" + user.user_Remark + "\"}]}";
        }
    }
}
