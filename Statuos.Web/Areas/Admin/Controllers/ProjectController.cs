using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Data;
using Statuos.Domain;
using Statuos.Service;
using Statuos.Web.Infrastructure.AutoMapper;
using Statuos.Web.Infrastructure.Helpers;
using Statuos.Web.Models;

namespace Statuos.Web.Areas.Admin.Controllers
{
    public class ProjectController : AdminController
    {
        private IRepository<Project> _projectRepository;
        private IProjectService _projectService;
        private IRepository<Customer> _customerRepository;
        private IRepository<User> _userRepository;
        public ProjectController(IRepository<Project> projectRepository, IProjectService projectService, IRepository<Customer> customerRepository, IRepository<User> userRepository)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        private void SetupCustomerViewBag()
        {
            ViewBag.Customers = new SelectList(_customerRepository.All.ToList(), "Id", "Name"); 
        }

        private void SetupProjectTypesViewBag()
        {
            List<TypeViewModel> types = TypeHelper.GetTypes<ProjectViewModel>();
            ViewBag.ProjectTypes = new SelectList(types, "Type", "Name");
        }
        //
        // GET: /Admin/Project/

        public ActionResult Index()
        {
            SetupProjectTypesViewBag();
            var projects = _projectRepository.All.ToList();
            var projectVMs = projects.MapTo<ProjectViewModel>();
            return View(projectVMs);
        }

        //
        // GET: /Admin/Project/Details/5

        public ActionResult Details(int id = 0)
        {
            var project = _projectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project.MapTo<ProjectViewModel>());
        }

        

        //
        // GET: /Admin/Project/Create

        public ActionResult CreateProjectType(ProjectViewModel projectviewmodel = null)
        {
            SetupCustomerViewBag();
            return View("Create", projectviewmodel);
        }

        

        //
        // POST: /Admin/Project/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel projectviewmodel)
        {
            var project = projectviewmodel.MapTo<Project>();
            var user = _userRepository.All.Where(u => u.UserName == projectviewmodel.UserName).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("UserName", "There username entered does not exist in the system");
            }
            if (ModelState.IsValid)
            {
                project.ProjectManagerId = user.Id;
                _projectService.Add(project);
                return RedirectToAction("Index");
            }
            return CreateProjectType(projectviewmodel);
        }

        //
        // GET: /Admin/Project/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SetupCustomerViewBag();
            Project project = _projectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project.MapTo<ProjectViewModel>());
        }

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel projectviewmodel)
        {
            if (ModelState.IsValid)
            {
                var project = projectviewmodel.MapTo<Project>();
                project.ProjectManagerId = _userRepository.All.Where(u => u.UserName == projectviewmodel.UserName).FirstOrDefault().Id;
                _projectService.Edit(project);
                return RedirectToAction("Index");
            }
            return View(projectviewmodel);
        }

        //
        // GET: /Admin/Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Project project = _projectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project.MapTo<ProjectViewModel>());
        }

        //
        // POST: /Admin/Project/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = _projectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ValidateProjectHasNoTasks(project);
            if (ModelState.IsValid)
            {
                _projectService.Delete(project);
                return RedirectToAction("Index");
            }
            return View(project.MapTo<ProjectViewModel>());
        }

        private void ValidateProjectHasNoTasks(Project project)
        {
            if (project.Tasks.Count > 0)
                ModelState.AddModelError("", "The Project has tasks and can not be deleted");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}