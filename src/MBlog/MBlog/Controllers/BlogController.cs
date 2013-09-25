using System.Web.Mvc;


namespace MBlog.Controllers
{
    public class BlogController : MongoController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
