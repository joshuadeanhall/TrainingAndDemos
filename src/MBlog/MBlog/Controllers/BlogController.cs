using System.Web.Mvc;
using MBlog.Models;


namespace MBlog.Controllers
{
    public class BlogController : MongoController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            return View(new PostDetailsViewModel {Id = id});
        }
    }
}
