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

        public ProjectController(IRepository<Project> projectRepository, IProjectService projectService)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
        }
        //
        // GET: /Project/


        public ActionResult Index()
        {
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
            return View(project.MapTo<ProjectViewModel>());
        }

        private void SetupTaskTypesViewBag()
        {
            ViewBag.TaskTypes = new SelectList(TypeHelper.GetTypes<TaskViewModel>(), "Type", "Name");
        }
        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}