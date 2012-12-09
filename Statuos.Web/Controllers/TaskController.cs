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
using Statuos.Data.Queries;

namespace Statuos.Web.Controllers
{
    public class TaskController : Controller
    {
        private IRepository<Task> _taskRepository;
        private ITaskService _taskService;
        private IRepository<User> _userRepository;
        


        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, IRepository<User> userRepository)
        {
            _taskRepository = taskRepository;
            _taskService = taskService;
            _userRepository = userRepository;
        }
        //
        // GET: /Task/

        public ActionResult Index()
        {
            //TODO clean this up would prefer to do this based on username and not on actually passing a user
            
            var tasks = _taskRepository.All.UserIsAssignedToTaskQuery(User.Identity.Name);
            return View(tasks.ToList().MapTo<TaskViewModel>());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task ta = _taskRepository.All.Where(t => t.Id == id).FirstOrDefault();
            Task task = _taskRepository.All.Where(t => t.Id == id).UserIsAssignedToTaskQuery(User.Identity.Name).FirstOrDefault();
            
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task.MapTo<TaskViewModel>());
        }

        public ActionResult CreateTaskType(TaskViewModel task)
        {
            return View("Create", task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel task)
        {            
            var taskModel = task.MapTo<Task>();
            //TODO Verify user is manager of project 
            var currentUser = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var isProjectManager = currentUser.Projects.Any(p => p.Id == task.ProjectId);
            if (!isProjectManager)
            {
                return HttpNotFound();
            }
            var userNames = task.UserNames.Split(';');
            taskModel.Users = new List<User>();
            foreach(var userName in userNames)
            {
                var user = _userRepository.All.Where(u => u.UserName == userName).FirstOrDefault();
                taskModel.Users.Add(user);
            }
            _taskService.Add(taskModel);
            return RedirectToAction("Details", "Project", new { id = taskModel.ProjectId });
        }


        public ActionResult Edit(int id = 0)
        {
            var currentUser = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            
            var task = _taskRepository.Find(id);
            if (task == null)
                return HttpNotFound();
            if (!currentUser.Projects.Any(p => p.Id == task.ProjectId))
                return HttpNotFound();

            return View(task.MapTo<TaskViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var task = taskViewModel.MapTo<Task>();
                var currentUser = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (!currentUser.Projects.Any(p => p.Id == taskViewModel.ProjectId))
                    return HttpNotFound();

                _taskService.Edit(task);
                return RedirectToAction("Details", "Project", new { id = task.ProjectId });

            }
            return View(taskViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}