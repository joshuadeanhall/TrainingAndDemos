using System.Web.Mvc;
using MBlog.Areas.Admin.Models;
using MBlog.Controllers;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : AdminBaseController
    {
        public ActionResult Index()
        {
            var users = Database.GetCollection<User>("users").FindAll();
            var usersViewModel = users.MapTo<UserViewModel>();
            return View(usersViewModel);
        }

        public ActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = Database.GetCollection<User>("users");
                collection.Insert(userViewModel.MapTo<User>());
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }


        public ActionResult Edit(object id)
        {
            var collection = Database.GetCollection<User>("users");
            var user = collection.FindOneById(new BsonObjectId(id.ToString()));
            return View(user.MapTo<UserViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = Database.GetCollection<User>("users");
                collection.Update(Query.EQ("_id", new BsonObjectId(userViewModel.UserId)),
                    Update.Set("Password", userViewModel.Password));
                return RedirectToAction("Index");
            }
            return View(userViewModel);

        }

        public ActionResult Delete(object id)
        {
            var collection = Database.GetCollection<User>("users");
            var user = collection.FindOneById(new BsonObjectId(id.ToString()));
            return View(user.MapTo<UserViewModel>());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(object id)
        {

            var collection = Database.GetCollection<User>("users");
            collection.Remove(Query.EQ("_id", new BsonObjectId(id.ToString())));
            return RedirectToAction("Index");
        }
    }
}