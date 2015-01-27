using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statuos.Web.Controllers;
using Moq;
using Statuos.Domain;
using Statuos.Data;
using Statuos.Service;
using System.Web.Mvc;
using Statuos.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;
using AutoMapper;
using Statuos.Web.Infrastructure.AutoMapper.Profiles;

namespace Statuos.Test.Controllers
{
    [TestClass]
    public class TaskControllerTests
    {
        private TaskController controller;
        private Mock<IRepository<Task>> taskRepository;
        private Mock<ITaskService> taskService;
        private Mock<IRepository<User>> userRepository;
        private Mock<IRepository<Project>> projectRepository;
        private Mock<IProjectService> projectService;
        private int invalidTaskId = 11;
        private int validTaskId = 1;
        private Task validTask;
        private TaskViewModel invalidTaskViewModel;
        private TaskViewModel validTaskViewModel;
        private Project invalidProject;

        public TaskControllerTests()
        {
            Mapper.AddProfile(new UserProfile());
            Mapper.AddProfile(new CustomerProfile());
            Mapper.AddProfile(new ProjectProfile());
            Mapper.AddProfile(new TaskProfile());

            invalidProject = new BasicProject() { CustomerId = 1, EstimatedHours = 5.0M, Id = 11, ProjectManager = new User() { UserName = "user1" } };
            var users = new List<User>() 
                { 
                    new User() { 
                        Id = 1, UserName = "jdhall", 
                        Projects = new List<Project>() { 
                            new BasicProject() { Id = 1 } 
                        } 
                    } 
                }.AsQueryable();
            invalidTaskViewModel = new BasicTaskViewModel() { EstimatedHours = 3.00M, Id = 1, Project = new TaskViewModel.ProjectDetails() { Id = 11 }, Title = "TaskTitle" };
            validTask = new BasicTask()
            {
                Id = validTaskId,
                EstimatedHours = 3.00M,
                ProjectId = 1,
                Title = "Task Title",
                Users = users.ToList(),
                CompletedDetails = null,
                Project = new BasicProject()
                {
                    Id = 1,
                    ProjectManager = new User()
                    {
                        IsActive = true,
                        Id = 1,
                        UserName = "jdhall"
                    }
                }
            };
            validTaskViewModel = (BasicTaskViewModel)Mapper.Map(validTask, validTask.GetType(), typeof(TaskViewModel));
            var tasks = new List<Task>() { validTask }.AsQueryable();
            taskRepository = new Mock<IRepository<Task>>();
            taskRepository.Setup(t => t.All).Returns(tasks);
            taskRepository.Setup(t => t.Find(invalidTaskId)).Returns((Task)null);
            taskRepository.Setup(t => t.Find(validTaskId)).Returns(validTask);
            projectService = new Mock<IProjectService>();
            taskService = new Mock<ITaskService>();
            userRepository = new Mock<IRepository<User>>();
            projectRepository = new Mock<IRepository<Project>>();

            userRepository.Setup(u => u.All).Returns(users);
            controller = new TaskController(taskRepository.Object, taskService.Object, userRepository.Object, projectRepository.Object, projectService.Object);

            SetupControllerContext(controller);
        }

        private void SetupControllerContext(TaskController controller)
        {
            Mock<IPrincipal> principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity.Name).Returns("jdhall");
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("POST");
            request.Setup(r => r.Headers).Returns(new NameValueCollection());
            request.Setup(r => r.Form).Returns(new NameValueCollection());
            request.Setup(r => r.QueryString).Returns(new NameValueCollection());

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(request.Object);
            mockHttpContext.Setup(c => c.User).Returns(principal.Object);
            var controllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), new Mock<ControllerBase>().Object);
            controller.ControllerContext = controllerContext;
        }
        [TestMethod]
        public void IndexReturnsViewWithListOfTaskViewModel()
        {
            var result = (ViewResult)controller.Index();
            Assert.IsInstanceOfType(result.Model, typeof(List<TaskViewModel>));
        }

        [TestMethod]
        public void DetailsReturnsNotFoundForInvalidId()
        {
            var result = controller.Details(invalidTaskId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsReturnsViewWithValidTaskViewModel()
        {

            Mock<IRepository<Task>> taskRepository = new Mock<IRepository<Task>>();
            Mock<ITaskService> taskService = new Mock<ITaskService>();
            Mock<IRepository<User>> userRepository = new Mock<IRepository<User>>();
            Mock<IRepository<Project>> projectRepository = new Mock<IRepository<Project>>();
            Mock<IProjectService> projectService = new Mock<IProjectService>();
            var tasks = new List<BasicTask>();
            var task = new BasicTask()
            {
                Id = 1,
                EstimatedHours = 2m,
                Title = "Title",
                Project = new BasicProject()
                {
                    Id = 1,
                    ProjectManager = new User()
                    {
                        Id = 1,
                        IsActive = true,
                        UserName = "jdhall"
                    }
                },
                Users = new List<User>()
            };
            tasks.Add(task);
            taskRepository.Setup(t => t.All).Returns(tasks.AsQueryable());

            TaskController controller = new TaskController(taskRepository.Object, taskService.Object, userRepository.Object, projectRepository.Object, projectService.Object);
            SetupControllerContext(controller);
            var result = (ViewResult)controller.Details(validTaskId);
            var model = (TaskViewModel)result.Model;

            Assert.AreEqual(validTaskId, model.Id);
        }

        [TestMethod]
        public void OnlyProjectManagerCanCreateTask()
        {
            //TODO This test doesn't really validate this need to make this better
            var result = controller.Create(invalidTaskViewModel);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void CreateCallsProjectServiceEdit()
        {
            Mock<IRepository<Task>> taskRepository = new Mock<IRepository<Task>>();
            Mock<ITaskService> taskService = new Mock<ITaskService>();
            Mock<IRepository<User>> userRepository = new Mock<IRepository<User>>();
            Mock<IRepository<Project>> projectRepository = new Mock<IRepository<Project>>();
            Mock<IProjectService> projectService = new Mock<IProjectService>();

            var project = new BasicProject() { Id = 1, EstimatedHours = 3m, Title = "TestProject", ProjectManager = new User() { UserName = "jdhall" }, Tasks = new List<Task>() };
            projectRepository.Setup(p => p.Find(1)).Returns(project);
            userRepository.Setup(u => u.All).Returns(GetIQueryableUser());
            var controller = new TaskController(taskRepository.Object, taskService.Object, userRepository.Object, projectRepository.Object, projectService.Object);
            SetupControllerContext(controller);

            var viewModel = new BasicTaskViewModel() { Id = 1, Title = "TestTask", UserNames = "jdhall", EstimatedHours = 4m };
            viewModel.Project = new TaskViewModel.ProjectDetails() { Id = 1, CustomerName = "TST", EstimatedHours = 2m, Title = "TestProject" };
            var result = controller.Create(viewModel);
            projectService.Verify(ps => ps.Edit(It.Is<Project>(p => p.Tasks.Any(t => t.Id == 1))), Times.Once(), "ProjectEdit not called");
        }

        [TestMethod]
        public void AddUserWithIdReturnsValidViewModel()
        {
            var result = (ViewResult)controller.AddUser(validTaskId);
            Assert.IsInstanceOfType(result.Model, typeof(TaskUserViewModel));
        }

        [TestMethod]
        public void AddUserWithViewModelCallsEditOnTaskServiceWithNewUser()
        {
            User user = new User() { Id = 1, UserName = "NewUser", IsActive = true };
            List<User> users = new List<User>();
            users.Add(user);
            TaskUserViewModel userViewModel = new TaskUserViewModel()
            {
                TaskId = validTaskId,
                UserName = user.UserName
            };
            userRepository.Setup(u => u.All).Returns(users.AsQueryable());
            controller.AddUser(userViewModel);

            taskService.Verify(ts => ts.Edit(It.Is<Task>(t => t.Users.Contains(user))));
        }

        [TestMethod]
        public void DeleteUserFromTaskWithValidUserAndTaskCallsDeleteAssignedUser()
        {
            userRepository.Setup(u => u.Find(1)).Returns(new User() { Id = 1, UserName = "jdhall", IsActive = true });
            controller.DeleteUser(validTaskId, 1);
            taskService.Verify(ts => ts.DeleteAssignedUser(It.Is<Task>(t => t.Id == validTaskId), It.Is<User>(u => u.Id == 1)));
        }

        [TestMethod]
        public void DeleteUserFromTaskWithInvaliduserReturnsNotFound()
        {
            userRepository.Setup(u => u.Find(11)).Returns((User)null);
            var result = controller.DeleteUser(validTaskId, 11);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteUserFromTaskWithInvalidTaskReturnsNotFound()
        {
            taskRepository.Setup(u => u.Find(validTaskId)).Returns((Task)null);
            userRepository.Setup(u => u.Find(1)).Returns(new User() { Id = 1, UserName = "jdhall", IsActive = true });
            var result = controller.DeleteUser(validTaskId, 1);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditTaskWithIdReturnsValidViewModel()
        {
            var result = (ViewResult)controller.Edit(validTaskId);
            Assert.IsInstanceOfType(result.Model, typeof(TaskViewModel));
        }

        [TestMethod]
        public void EditTaskWithInvalidIdReturnsNotFound()
        {
            var result = controller.Edit(invalidTaskId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditTaskWithValidIdAsNotAProjectManagerReturnsNotFound()
        {
            var task = new BasicTask()
            {
                Id = validTaskId,
                Project = new BasicProject()
                {
                    Id = 1,
                    ProjectManager = new User()
                    {
                        Id = 1,
                        IsActive = true,
                        UserName = "NotCurrentUser"
                    }
                }
            };
            
            taskRepository.Setup(t => t.Find(validTaskId)).Returns(task);
            var result = controller.Edit(validTaskId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditTaskWithValidTaskViewModelPassesValidTaskToEdit()
        {
            validTaskViewModel.Title = "TitleEdit";
            controller.Edit(validTaskViewModel);
            taskService.Verify(ts => ts.Edit(It.Is<Task>(t => t.Title == "TitleEdit")));
            
        }

        [TestMethod]
        public void EditTaskWithCurrentUserNotAProjectManagerReturnsNotFound()
        {
            validTask.Project.ProjectManager.UserName = "NotCurrentUser";
            taskRepository.Setup(t => t.Find(validTask.Id)).Returns(validTask);
            var result = controller.Edit(validTaskViewModel);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void CompleteTaskWithValidIdMarksTaskCompleteAndCallEditService()
        {            
            controller.CompleteTask(validTaskId);
            taskService.Verify(ts => ts.Edit(It.Is<Task>(t => t.CompletedDetails != null)));
            
        }
        private IQueryable<User> GetIQueryableUser()
        {
            var users = new List<User>() 
                { 
                    new User() { 
                        Id = 1, UserName = "jdhall", 
                        Projects = new List<Project>() { 
                            new BasicProject() { Id = 1 } 
                        } 
                    } 
                }.AsQueryable();

            return users;
        }

    }
}
