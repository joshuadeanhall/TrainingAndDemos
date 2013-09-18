using System;
using System.Web.Mvc;
using MBlog.Domain;


namespace MBlog.Controllers
{
    public class HomeController : MongoController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
