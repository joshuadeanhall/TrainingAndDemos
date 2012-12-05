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
            ViewBag.ProjectTypes = new SelectList(TypeHelper.GetTypes<ProjectViewModel>(), "Type", "Name");
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

        public ActionResult CreateProjectType(ProjectViewModel projectviewmodel)
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
            if (ModelState.IsValid)
            {
                var project = projectviewmodel.MapTo<Project>();
                project.ProjectManagerId = _userRepository.All.Where(u => u.UserName == projectviewmodel.UserName).FirstOrDefault().Id;
                _projectService.Add(project);                
                return RedirectToAction("Index");
            }
            return View(projectviewmodel);
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
            _projectService.Delete(project);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}