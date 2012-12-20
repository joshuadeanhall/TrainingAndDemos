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
            validTask = new BasicTask() { Id = validTaskId, EstimatedHours = 3.00M, ProjectId = 1, Title = "Task Title", Users = users.ToList() };
            validTaskViewModel = (BasicTaskViewModel)Mapper.Map(validTask, validTask.GetType(), typeof(TaskViewModel));
            var tasks = new List<Task>() { validTask  }.AsQueryable();
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
            
            SetupControllerContext();
        }

        private void SetupControllerContext()
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
        public void CreateCallsTaskServiceAdd()
        {
            validTaskViewModel.UserNames = "";
            var result = controller.Create(validTaskViewModel);
            taskService.Verify(ts => ts.Add(It.Is<Task>(t => t.Id == validTaskViewModel.Id)), Times.Once(), "Add service method not called once");
        }

    }
}
