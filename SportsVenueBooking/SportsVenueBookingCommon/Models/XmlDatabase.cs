/*
 * 作者：李贵发
 * 时间：2015-04-09
 * 功能：xml数据的操作
 * 文件：XmlDatabase.cs
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SportsVenueBookingCommon.Models
{
    /// <summary>
    /// xml操作类
    /// </summary>
    public class XmlDatabase<T> where T : class
    {
        /// <summary>
        /// 获取xml路径
        /// </summary>
        private static readonly string xmlConn = HttpRuntime.AppDomainAppPath + ConfigurationManager.ConnectionStrings["sysConfig"].ToString();

        /// <summary>
        /// XDocument对象，对xml文件进行操作
        /// </summary>
        private XDocument xdoc = null;

        /// <summary>
        /// 构造函数，创建对象
        /// </summary>
        public XmlDatabase()
        {
            xdoc = XDocument.Load(xmlConn);
        }

        /// <summary>
        /// 查询指定名称的标签下的文本
        /// </summary>
        /// <param name="s">要查询的名称</param>
        /// <returns>查询结果</returns>
        public string Select(Expression<Func<T, string>> s)
        {
            var res = from sys in xdoc.Descendants(s.Body.ToString().Split('.')[1]) select sys.Value;
            return res.Count() >= 1 ? res.First() : String.Empty;
        }
    }
}
