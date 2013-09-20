using System.Web.Mvc;
using MBlog.Areas.Admin.Models;
using MBlog.Controllers;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;

namespace MBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : MongoController
    {
        public ActionResult Index()
        {
            var users = Database.GetCollection<User>("users").FindAll();
            var usersViewModel = users.MapTo<UserViewModel>();
            return View(usersViewModel);
        }

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Edit()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Details()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}