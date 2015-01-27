using System.Web.Mvc;

namespace MBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : AdminBaseController
    {
        //
        // GET: /Admin/Admin/
        public ActionResult Index()
        {
            return View();
        }

    }
}
