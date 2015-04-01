﻿using SportsVenueBookingBLL;
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
            return new User().GetUserInfo(tId);
        }

        /// <summary>
        /// 读取教师预约界面
        /// </summary>
        /// <returns>教师预约界面</returns>
        [HttpPost]
        public PartialViewResult BookingSite()
        {
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
        /// <returns>场地预约情况Json数据</returns>
        [HttpPost]
        public string GetAppointmentInfo(string startDate, string endDate, string conditions, string type, string time)
        {
            return new Reservation().GetAppointmentInfo(startDate, endDate, conditions, type, time);
        }

        [HttpPost]
        public JsonResult ReservationIn(string duration, string space, string start, string end)
        {
            return Json(new Reservation().ReservationIn(duration, space, start, end, HttpContext.Session["techer"].ToString()));
        }
    }
}