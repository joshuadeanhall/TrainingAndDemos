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
        private IRepository<Project> _projectRepository;


        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, IRepository<User> userRepository, IRepository<Project> projectRepository)
        {
            _taskRepository = taskRepository;
            _taskService = taskService;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
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
            //TODO user is assigned to or is project manager
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
            return View("Create", task);
        }

        public ActionResult AddUser(int id = 0)
        {
            var task = _taskRepository.Find(id);
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
                _taskService.Add(taskModel);
                return RedirectToAction("Details", "Project", new { id = taskModel.ProjectId });
            }
            return View(task);
        }

        //This is not a post and should be converted to using post in the future.  Doing a confirm delete page would be a simple way to accomplish this.
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
            if (!currentUser.Projects.Any(p => p.Id == task.ProjectId))
                return HttpNotFound();

            return View(task.MapTo<TaskViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel taskViewModel)
        {
            VerifyProjectId(taskViewModel);
            if (ModelState.IsValid)
            {
                //var task = taskViewModel.MapTo<Task>();
                var task = _taskRepository.Find(taskViewModel.Id);
                task.Title = taskViewModel.Title;
                task.EstimatedHours = taskViewModel.EstimatedHours;
                var currentUser = _userRepository.All.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (!currentUser.Projects.Any(p => p.Id == taskViewModel.Project.Id))
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