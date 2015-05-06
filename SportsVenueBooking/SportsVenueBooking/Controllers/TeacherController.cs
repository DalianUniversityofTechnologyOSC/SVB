using SportsVenueBookingBLL;
using SportsVenueBookingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace SportsVenueBooking.Controllers
{
    /// <summary>
    /// 教师预约模块控制器
    /// </summary>
    //[UserAuthorization("teacher", "/User/SubscribeLogin")]       //等待登录模板完成启用
    [OutputCache(Duration = 10)]
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/

        /// <summary>
        /// 显示教师预约主页面
        /// </summary>
        /// <returns>教师预约主页面</returns>
        [HttpGet]
        public ActionResult Index()
        {
            HttpContext.Session["techer"] = 1;
            return View();
        }

        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <returns>登录用户信息Json字符串</returns>
        [HttpPost]
        public string GetUserInfo()
        {
            long tId = Convert.ToInt64(HttpContext.Session["techer"].ToString());
            return new User().GetUserInfo<string>(tId,true);
        }

        /// <summary>
        /// 读取教师预约界面
        /// </summary>
        /// <returns>教师预约界面</returns>
        [HttpPost]
        public PartialViewResult BookingSite(string window_id)
        {
            ViewBag.weekNumber = new SysAttibute().GetSemesterWeekNumber();
            ViewBag.windowId = window_id;
            ViewBag.spaces = new Space().GetAllSpace();
            ViewBag.durations = new Duration().GetAllDuration();
            return PartialView();
        }

        /// <summary>
        /// 查询场地预约情况
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="type">场地类型</param>
        /// <param name="time">课程时间</param>
        /// <param name="dayOfWeek">课程星期</param>
        /// <param name="isWeek">是否是周查询</param>
        /// <returns>场地预约情况Json数据</returns>
        [HttpPost]
        public string GetAppointmentInfo(string startDate, string endDate, string conditions, string type, string time, string dayOfWeek, bool isWeek)
        {
            return new Reservation().GetAppointmentInfo(startDate, endDate, conditions, type, time, dayOfWeek, isWeek);
        }

        /// <summary>
        /// 执行预约操作
        /// </summary>
        /// <param name="duration">要预约的课程时间</param>
        /// <param name="space">要预约的场地</param>
        /// <param name="start">预约开始时间</param>
        /// <param name="end">预约结束时间</param>
        /// <returns>预约结果Json字符串</returns>
        [HttpPost]
        public JsonResult ReservationIn(string duration, string space, string start, string end, string dayOfWeek)
        {
            return Json(new Reservation().ReservationIn(duration, space, start, end, HttpContext.Session["techer"].ToString(), dayOfWeek));
        }

        /// <summary>
        /// 返回预约情况页面
        /// </summary>
        /// <param name="duration">要查看的课程时间</param>
        /// <param name="space">要查看的场地</param>
        /// <returns>预约情况页面</returns>
        [HttpPost]
        public PartialViewResult SearchReservation(string duration, string space, string startDate, string endDate, string dayOfWeek)
        {
            ViewBag.duration = duration;
            ViewBag.space = space;
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.dayOfWeek = dayOfWeek;
            return PartialView();
        }

        /// <summary>
        ///查询预约情况数据
        /// </summary>
        /// <param name="duration">要查看的课程时间</param>
        /// <param name="space">要查看的场地</param>
        /// <returns>预约情况数据</returns>
        [HttpPost]
        public string SearchReservationJson(string duration, string space, string startDate, string endDate, string dayOfWeek)
        {
            return new Reservation().SearchReservationJson(duration, space, startDate, endDate, dayOfWeek);
        }

        /// <summary>
        /// 显示我的预约页
        /// </summary>
        /// <returns>我的预约页</returns>
        [HttpPost]
        public PartialViewResult MyReservation()
        {
            return PartialView();
        }

        [HttpPost]
        public string GetMyReservationData()
        {
            return new Reservation().GetMyReservationData(Convert.ToInt64(HttpContext.Session["techer"]));
        }

        /// <summary>
        /// 显示账号设置界面
        /// </summary>
        /// <returns>账号设置界面</returns>
        public ActionResult TeacherSet()
        {
            ViewBag.techerInfo = new User().GetUserInfo<SportsVenueBookingCommon.Models.User>(Convert.ToInt64(HttpContext.Session["techer"]));
            return View();
        }

        public JsonResult ModifyInfo()
        {
            return ModifyInfo();
        }
    }
}
