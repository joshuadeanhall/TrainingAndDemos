using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuos.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = @"home\statuos_admin")]
    public abstract class AdminController : Controller
    {
    }
}