using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBlog.Areas.Admin.Controllers
{
    public class PostController : AdminBaseController
    {
        //
        // GET: /Admin/Post/

        public ActionResult Index()
        {
            return View();
        }

    }
}
