using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Statuos.Domain;
using Statuos.Data;
using Statuos.Service;
using Statuos.Web.Infrastructure.AutoMapper;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Models;

namespace Statuos.Web.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private IRepository<User> _userRepository;
        private IUserService _userService;
        public UserController(IRepository<User> userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View(_userRepository.All.ToList().MapTo<UserViewModel>());
        }

        //
        // GET: /Admin/User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = _userRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.MapTo<UserViewModel>());
        }

        //
        // GET: /Admin/User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                user.IsActive = true;
                _userService.Add(user.MapTo<User>());
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Admin/User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = _userRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.MapTo<UserViewModel>());
        }

        //
        // POST: /Admin/User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(user.MapTo<User>());
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = _userRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.MapTo<UserViewModel>());
        }

        //
        // POST: /Admin/User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = _userRepository.Find(id);
            if (user == null)
                return HttpNotFound();

            VerifyUserCanBeDeleted(user);
            if (ModelState.IsValid)
            {
                _userService.Delete(user);
                return RedirectToAction("Index");
            }
            return View(user.MapTo<UserViewModel>());
        }

        private void VerifyUserCanBeDeleted(User user)
        {
            if (user.Tasks.Count > 0 || user.Projects.Count > 0)
            {
                ModelState.AddModelError("", "User can not be deleted because the user is assigned to tasks or is a PM");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}