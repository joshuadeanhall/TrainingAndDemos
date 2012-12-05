using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statuos.Web.Controllers;
using Moq;
using Statuos.Domain;
using Statuos.Data;
using Statuos.Service;
using Statuos.Web.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Statuos.Web.Infrastructure.AutoMapper.Profiles;

namespace Statuos.Test.Controllers
{
    [TestClass]
    public class ProjectControllerTests
    {
        private ProjectController controller;
        private Mock<IRepository<Project>> projectRepository;
        private Mock<IProjectService> projectService;
        private int invalidProjectId = 11;
        private int validProjectId = 1;
        private Project project;
        private ProjectViewModel projectViewModel;
        private Mock<HttpContextBase> context;

        public ProjectControllerTests()
        {
            Mapper.AddProfile(new UserProfile());
            Mapper.AddProfile(new CustomerProfile());
            Mapper.AddProfile(new ProjectProfile());
            Mapper.AddProfile(new TaskProfile());
            var projectManager = new User() { Id = 1, UserName = "jdhall" };
            var projects = new List<Project>() { new BasicProject() { Id = validProjectId, CustomerId = 1, Title = "Project 1", ProjectManager = projectManager } }.AsQueryable();
            projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(p => p.All).Returns(projects);
            projectService = new Mock<IProjectService>();
            controller = new ProjectController(projectRepository.Object, projectService.Object);

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
        public void IndexReturnsProjectViewModelsForManager()
        {
           var result =  controller.Index();
           Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DetailsReturnsNotFoundForInvalidId()
        {
            var result = controller.Details(invalidProjectId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailReturnViewWithValidProjectViewModel()
        {
            var result = (ViewResult)controller.Details(validProjectId);
            var model = (ProjectViewModel)result.Model;
            Assert.AreEqual(validProjectId, model.Id);            
        }

    }
}
