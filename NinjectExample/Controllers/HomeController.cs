using System;
using System.Web.Mvc;
using NinjectExample.Services;

namespace NinjectExample.Controllers
{
    public class HomeController : Controller
    {
        //Todo Modules
        //Todo GetvsCreate
        private readonly ICarServiceFactory _carServiceFactory;

        public HomeController(ICarServiceFactory carServiceFactory)
        {
            if (carServiceFactory == null) throw new ArgumentNullException("carServiceFactory");
            _carServiceFactory = carServiceFactory;
        }

        public ActionResult Index()
        {
            var gmService = _carServiceFactory.GetGmService();

            var bmwService = _carServiceFactory.CreateBmwService();
            var services = _carServiceFactory.CreateAllCarServices();
            return View();
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
}