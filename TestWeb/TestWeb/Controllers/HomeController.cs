using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Data;
using TestWeb.Messages;

namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        public ActionResult SendCommand()
        {
            var context = new TestWebContext();
            var rand = new Random();
            context.Examples.Add(new Example {Name = "joshua" + rand.Next(100)});
            context.SaveChanges();
            context.Dispose();
            MvcApplication.Bus.Send(new SendCommandMessage {Id = Guid.NewGuid(), Name = "NewName"});
           return RedirectToAction("Index");
        }
    }
}
