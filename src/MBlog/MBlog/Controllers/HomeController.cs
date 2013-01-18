using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MBlog.Domain;
using System.Configuration;



namespace MBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings.Get("MongoUrl"));
            var server = client.GetServer();
            MongoCredentials creds = new MongoCredentials(ConfigurationManager.AppSettings.Get("User"), ConfigurationManager.AppSettings.Get("Password"));
            var database = server.GetDatabase(ConfigurationManager.AppSettings.Get("Database"), creds);

            var collection = database.GetCollection<Blog>("blogs");
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
