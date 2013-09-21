using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBlog.Areas.Admin.Models;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;

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

                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        public ActionResult Edit(string id)
        {
            return View(new PostViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        public ActionResult Delete(string id)
        {
            return View(new PostViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(postViewModel.PostId);
        }

        public ActionResult Preview(string id)
        {
            return View(new PostViewModel());
        }

    }
}
