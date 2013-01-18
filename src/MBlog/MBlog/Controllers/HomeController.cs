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
            var url = ConfigurationManager.AppSettings.Get("MongoUrl");
            var username = ConfigurationManager.AppSettings.Get("User");
            var password = ConfigurationManager.AppSettings.Get("Password");
            var databaseName = ConfigurationManager.AppSettings.Get("Database");

            //var client = new MongoClient(url);
            //var server = client.GetServer();
            //MongoCredentials creds = new MongoCredentials(username, password);
            //var database = server.GetDatabase(databaseName, creds);

            //var collection = database.GetCollection<Blog>("blogs");
            var blog = new Blog {PublishDate = DateTime.Now, Title = "First Blog" };

            //collection.Insert(blog);

            ViewBag.Message = string.Format("Modify this template to jump-start your ASP.NET MVC application. {0} ::: {1} ::: {2} ::: {3}", url, username, password, databaseName);

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
