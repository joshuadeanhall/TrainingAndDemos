using System;
using System.Web.Mvc;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MBlog.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MBlog.Areas.Admin.Controllers
{
    public class PostController : AdminBaseController
    {

        //
        // GET: /Admin/Post/

        public ActionResult Index()
        {
            var collection = Database.GetCollection<Post>("posts");
            var posts = collection.FindAll();
            return View(posts.MapTo<PostViewModel>());
        }

        public ActionResult Create()
        {
            return View(new PostViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                postViewModel.PublishDate = DateTime.Now;
                var collection = Database.GetCollection<Post>("posts");
                collection.Insert(postViewModel.MapTo<Post>());
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        public ActionResult Edit(string id)
        {
            var collection = Database.GetCollection<Post>("posts");
            var post = collection.FindOneById(new BsonObjectId(id));
            return View(post.MapTo<PostViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = Database.GetCollection<Post>("posts");
                var update = Update.Set("Title", postViewModel.Title);
                update.Set("Content", postViewModel.Content);
                collection.Update(Query.EQ("_id", new BsonObjectId(postViewModel.PostId)), update);
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        public ActionResult Delete(string id)
        {
            var collection = Database.GetCollection<Post>("posts");
            var post = collection.FindOneById(new BsonObjectId(id));
            return View(post.MapTo<PostViewModel>());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var collection = Database.GetCollection<Post>("posts");
            collection.Remove(Query.EQ("_id", new BsonObjectId(id)));
            return RedirectToAction("Index");
        }

        public ActionResult Preview(string id)
        {
            var collection = Database.GetCollection<Post>("posts");
            var post = collection.FindOneById(new BsonObjectId(id));
            return View(post.MapTo<PostViewModel>());
        }

    }
}
