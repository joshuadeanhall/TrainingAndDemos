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
using Statuos.Web.Models;
using Statuos.Web.Infrastructure.Helpers;
using Statuos.Data.Queries;

namespace Statuos.Web.Controllers
{
    public class ProjectController : Controller
    {
        private IRepository<Project> _projectRepository;
        private IProjectService _projectService;
        private IRepository<User> _userRepository;

        public ProjectController(IRepository<Project> projectRepository, IProjectService projectService, IRepository<User> userRepository)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
            _userRepository = userRepository;
        }
        //
        // GET: /Project/


        public ActionResult Index()
        {
            throw new Exception("Error occured for " + User.Identity.Name);
            var projects = _projectRepository.All.Include(p => p.Customer).UserIsProjectManagerQuery(User.Identity.Name);
            return View(projects.ToList().MapTo<ProjectViewModel>());
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(int id = 0)
        {
            SetupTaskTypesViewBag();
            //TODO improve method for checking if user is manager
            Project project = _projectRepository.All.Include(p => p.Tasks).Where(p => p.Id == id).UserIsProjectManagerQuery(User.Identity.Name).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            var projectVM = project.MapTo<ProjectViewModel>();
            projectVM.Charges = project.Charges.MapTo<ProjectViewModel.ProjectChargeDetails>();
            return View(projectVM);//project.MapTo<ProjectViewModel>());
        }

        private void SetupTaskTypesViewBag()
        {
            ViewBag.TaskTypes = new SelectList(TypeHelper.GetTypes<TaskViewModel>(), "Type", "Name");
        }

        //TODO Need to make this a post and also verify the manager is active.
        public ActionResult CompleteProject(int id = 0)
        {
            var project = _projectRepository.Find(id);
            var user = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            project.MarkComplete(user);
            _projectService.Edit(project);
            return RedirectToAction("Index");
            

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}