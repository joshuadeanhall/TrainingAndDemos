﻿using System;
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
        private IRepository<Project> _projectRepository;
        private IProjectService _projectService;

        //I started with the goal of seperating the code into repositories and services and this appears to be the result of that abstraction.  I could have just been passing in a 
        //IDbContext and using that instead of all these service and repositories
        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, IRepository<User> userRepository, IRepository<Project> projectRepository, IProjectService projectService)
        {
            _taskRepository = taskRepository;
            _taskService = taskService;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _projectService = projectService;
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
            //TODO clean this up, should handle this when moving most of these operations to dialogs.
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            //Get task if user is allowed to work on the task.
            Task task = _taskRepository.All.Where(t => t.Id == id).UserIsAssignedToTaskQuery(User.Identity.Name).FirstOrDefault();

            if (task == null)
            {
                return HttpNotFound();
            }
            var taskViewModel = task.MapTo<TaskViewModel>();
            //taskViewModel.Project = task.Project.MapTo<TaskViewModel.ProjectDetails>();
            return View(taskViewModel);
        }

        public ActionResult CreateTaskType(TaskViewModel task)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Create", task);
            }
            return View("Create", task);
        }

        public ActionResult AddUser(int id = 0)
        {
            var task = _taskRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddUser", task.MapTo<TaskUserViewModel>());
            }
            return View(task.MapTo<TaskUserViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(TaskUserViewModel taskUserViewModel)
        {
            Task task = _taskRepository.Find(taskUserViewModel.TaskId);
            if (task == null)
                return HttpNotFound();

            if (task.Project.ProjectManager.UserName != User.Identity.Name)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                User user = _userRepository.All.Where(u => u.UserName == taskUserViewModel.UserName).FirstOrDefault();

                task.Users.Add(user);
                _taskService.Edit(task);
                return RedirectToAction("Details", new { id = task.Id });
            }
            return View(taskUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel task)
        {
            var project = _projectRepository.Find(task.Project.Id);
            if (project == null || project.ProjectManager.UserName != User.Identity.Name)
            {
                return HttpNotFound("Project Id not found or you are not the PM");
            }
            var userNames = task.UserNames.Split(';');
            var taskModel = task.MapTo<Task>();
            taskModel.Users = GetUsers(taskModel, userNames);

            if (ModelState.IsValid)
            {

                if (project.AddTask(taskModel))
                {
                    _projectService.Edit(project);
                    return RedirectToAction("Details", "Project", new { id = taskModel.ProjectId });
                }
                else
                {
                    ModelState.AddModelError("Max Project Hours", "The estimated hours on this project would go over the allowed hours for the project.  Please revise the tasks estimated hours or increase the projects Max hours");
                }
            }
            return View(task);
        }

        //This is not a post and should be converted to using post in the future.  Doing a confirm delete page would be a simple way to accomplish this.
        //TODO Change this to just delete the user from the task then pass it to edit and move the 
        public ActionResult DeleteUser(int taskId, int userId)
        {
            var task = _taskRepository.Find(taskId);
            var user = _userRepository.Find(userId);
            //Task and user exists and the current user is the project manager oterwise return not found.
            if (task == null || user == null || task.Project.ProjectManager.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }

            _taskService.DeleteAssignedUser(task, user);
            return RedirectToAction("Details", new { id = taskId });
        }
        public ActionResult ChargeHours(int id)
        {
            ViewBag.Id = id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChargeHours");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChargeHours(int id, decimal hours)
        {
            var user = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var task = _taskRepository.All.Where(t => t.Id == id).UserIsAssignedToTaskQuery(User.Identity.Name).FirstOrDefault();
            if (task == null)
                return HttpNotFound();
            if (task.Project.CanChargeHours(hours))
            {
                task.ChargeHours(hours, user);
                _taskService.Edit(task);
            }
            else
            {
                ModelState.AddModelError("Max Hours", "Can't charge hours because it will go over the max limit of possible hours");
                TempData["ViewData"] = ViewData;
            }
            return RedirectToAction("Details", new { id = id });
        }
        private List<User> GetUsers(Task taskModel, string[] userNames)
        {
            var users = new List<User>();
            foreach (var userName in userNames)
            {
                var user = _userRepository.All.Where(u => u.UserName == userName).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("UserNames", string.Format("User {0} does not exist in the system", userName));
                }
                users.Add(user);
            }
            return users;
        }


        public ActionResult Edit(int id = 0)
        {
            var currentUser = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var task = _taskRepository.Find(id);
            if (task == null)
                return HttpNotFound();
            if (task.Project.ProjectManager.UserName != User.Identity.Name)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit", task.MapTo<TaskViewModel>());
            }
            return View(task.MapTo<TaskViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel taskViewModel)
        {
            VerifyProjectId(taskViewModel);
            if (ModelState.IsValid)
            {
                var task = _taskRepository.Find(taskViewModel.Id);
                task.Title = taskViewModel.Title;
                task.EstimatedHours = taskViewModel.EstimatedHours;
                if (task.Project.ProjectManager.UserName != User.Identity.Name)
                    return HttpNotFound();

                _taskService.Edit(task);
                return RedirectToAction("Details", "Project", new { id = task.ProjectId });

            }
            return View(taskViewModel);
        }

        public ActionResult CompleteTask(int id = 0)
        {
            var task = _taskRepository.Find(id);
            var user = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (task == null || user == null || !task.UserIsAssignedTo(user))
                return HttpNotFound();
            
            task.MarkComplete(user);
            _taskService.Edit(task);
            return RedirectToAction("Details", new { id = task.Id }); 
        }


        private void VerifyProjectId(TaskViewModel taskViewModel)
        {
            var originalTask = _taskRepository.Find(taskViewModel.Id);
            if (originalTask.ProjectId != taskViewModel.Project.Id)
            {
                ModelState.AddModelError("ProjectId", "You can not change Project Id of a task");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}