using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KO_Angular_Demo.Controllers
{
    public class KnockoutController : Controller
    {
        // GET: Knockout
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Example()
        {
            return View();
        }
    }
}
