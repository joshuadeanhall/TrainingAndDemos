using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KO_Angular_Demo.Controllers
{
    public class AngularController : Controller
    {
        // GET: Angular
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Example()
        {
            return View();
        }

        public ActionResult Resource()
        {
            return View();
        }

        public ActionResult Additional()
        {
            return View();
        }
    }
}
