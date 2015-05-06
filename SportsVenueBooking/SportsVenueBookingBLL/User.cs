using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsVenueBookingCommon;

namespace SportsVenueBookingBLL
{
    #region 用户对象操作+public class User : BaseBLL<SportsVenueBookingCommon.Models.User>
    /// <summary>
    /// 用户对象操作
    /// </summary>
    public class User : BaseBLL<SportsVenueBookingCommon.Models.User>
    {
        #region 以Json形式获取用户信息+public T GetUserInfo<T>(long tId, bool isJson = false) where T : class
        /// <summary>
        /// 以Json形式获取用户信息
        /// </summary>
        /// <param name="tId">用户Id</param>
        /// <param name="isJson">是否以json形式返回数据</param>
        /// <returns>Json格式的用户信息或User类型对象</returns>
        public T GetUserInfo<T>(long tId, bool isJson = false) where T : class
        {
            if (isJson)
            {
                return this.UserToJson(base.Search(d => d.user_Id == tId && d.user_IsDel == false).First()) as T;
            }
            else
            {
                return base.Search(d => d.user_Id == tId && d.user_IsDel == false).First() as T;
            }
        }
        #endregion

        #region 修改教师信息+public SportsVenueBookingCommon.StatusAttribute ModifyInfo(long u_Id, string u_Name, string u_Remark, string u_Pwd,params string u_Class)
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="u_Id">用户Id</param>
        /// <param name="u_Name">修改数据 用户姓名</param>
        /// <param name="u_Remark">修改备注 用户备注</param>
        /// <param name="u_Pwd">用户登陆密码</param>
        /// <returns>修改结果</returns>
        public SportsVenueBookingCommon.StatusAttribute ModifyInfo(long u_Id, string u_Name, string u_Remark, string u_Pwd, params string u_Class)
        {
            SportsVenueBookingCommon.StatusAttribute res = new SportsVenueBookingCommon.StatusAttribute();
            SportsVenueBookingCommon.Models.User user = this.GetUserInfo<SportsVenueBookingCommon.Models.User>(u_Id);
            if (user.user_Password == Md5(user.user_UserNumber, u_Pwd))
            {
                if (user.user_Name == u_Name && user.user_Remark == u_Remark)
                {
                    if (u_Name.Regular("\\w{1,10}"))
                    {
                        if (u_Remark.Regular(".{1,25}"))
                        {
                            if ((u_Class != null && u_Class.Regular(".{1,10}")) || u_Class == null)
                            {
                                user.user_Name = u_Name;
                                user.user_Remark = u_Remark;
                                user.user_Class = u_Class == null ? user.user_Class : u_Class;
                                if (base.Modify(user, new string[2] { "user_Name", "user_Remark" }) >= 1)
                                {
                                    res.status = true;
                                    res.message = "修改成功！";
                                }
                                else
                                {
                                    res.status = false;
                                    res.message = "修改失败！未知错误...";
                                }
                            }
                            else
                            {
                                res.status = false;
                                res.message = "修改失败！姓名格式错误...";
                            }
                        }
                        else
                        {
                            res.status = false;
                            res.message = "修改失败！班级内容过长...";
                        }
                    }
                    else
                    {
                        res.status = false;
                        res.message = "修改失败！备注内容过长...";
                    }
                }
                else
                {
                    res.status = false;
                    res.message = "修改失败！信息未修改...";
                }
            }
            else
            {
                res.status = false;
                res.message = "修改失败！登陆密码错误...";
            }
            return res;
        }
        #endregion

        public SportsVenueBookingCommon.StatusAttribute ModifyPwd(long u_Id, string u_Old_Pwd, string u_New_Pwd)
        {
            SportsVenueBookingCommon.StatusAttribute res = new SportsVenueBookingCommon.StatusAttribute();
            SportsVenueBookingCommon.Models.User user = this.GetUserInfo<SportsVenueBookingCommon.Models.User>(u_Id);
            if (user.user_Password == Md5(user.user_UserNumber, u_Old_Pwd))
            {
                user.user_Password = u_New_Pwd;
                if()                                                                                                                                                                                                                                        
                if (base.Modify(user, "user_Password") >= 1)
                {
                    res.status = true;
                    res.message = "修改成功！";
                }
                else
                {
                    res.status = false;
                    res.message = "修改失败！未知错误...";
                }
            }
            else
            {
                res.status = false;
                res.message = "修改失败！密码错误...";
            }
            return res;
        }

        #region 将数据信息转换成Json数据+private string UserToJson(SportsVenueBookingCommon.Models.User user)
        /// <summary>
        /// 将数据信息转换成Json数据
        /// </summary>
        /// <param name="user">用户数据</param>
        /// <returns>用户信息Json格式</returns>
        private string UserToJson(SportsVenueBookingCommon.Models.User user)
        {
            return "{\"total\":2,\"rows\":[{\"name\":\"用户编号\",\"value\":\"" + user.user_UserNumber + "\"},{\"name\":\"用户类别\",\"value\":\"" + (user.user_Type == 1 ? "学生" : user.user_Type == 2 ? "教师" : "校外人员") + "\"},{\"name\":\"用户姓名\",\"value\":\"" + user.user_Name + "\"},{\"name\":\"用户班级\",\"value\":\"" + user.user_Class + "\"},{\"name\":\"用户状态\",\"value\":\"" + (user.user_Status == true ? "锁定" : "正常") + "\"},{\"name\":\"备注\",\"value\":\"" + user.user_Remark + "\"}]}";
        }
        #endregion
    }
    #endregion
}
