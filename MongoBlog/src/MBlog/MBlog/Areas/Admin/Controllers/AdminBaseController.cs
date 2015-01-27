using System.Web.Mvc;
using MBlog.Controllers;

namespace MBlog.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class AdminBaseController : MongoController
    {
    }
}
