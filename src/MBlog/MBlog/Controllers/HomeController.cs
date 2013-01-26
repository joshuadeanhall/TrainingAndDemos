using System;
using System.Web.Mvc;
using MBlog.Domain;


namespace MBlog.Controllers
{
    public class HomeController : MongoController
    {
        public ActionResult Index()
        {
            var collection = Database.GetCollection<Blog>("posts");
            var blog = new Blog {PublishDate = DateTime.Now, Title = "First Blog" };

            collection.Insert(blog);

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
