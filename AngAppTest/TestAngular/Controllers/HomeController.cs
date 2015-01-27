using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAngular.Models;

namespace TestAngular.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var reportType = 5;
            ReportType enumReportType = (ReportType) reportType;
            GetValue(enumReportType);
            if (enumReportType == null)
                throw new Exception();
            return View();
        }

        private string GetValue(ReportType enumReportType)
        {
            string x = enumReportType.ToString();

            var stringValueAttributeArray = enumReportType.GetType().GetField(enumReportType.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (stringValueAttributeArray.Length <= 0)
                return (string)null;
            return stringValueAttributeArray[0].Value;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    internal class StringValueAttribute : StringValueBaseAttribute
    {
    }

    internal class StringValueBaseAttribute
    {
        public virtual string Value { get; protected set; }
    }
}