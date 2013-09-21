using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBlog.Controllers;

namespace MBlog.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class AdminBaseController : MongoController
    {
        
    }
}
