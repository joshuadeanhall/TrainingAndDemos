using AutoMapper;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Statuos.Data;
using Statuos.Domain;
using Statuos.Service;
using Statuos.Web;
using Statuos.Web.Areas.Admin.Controllers;
using Statuos.Web.Infrastructure.AutoMapper.Profiles;
using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Statuos.Test.Controllers.Admin
{
    [TestClass]
    public class ProjectControllerTests
    {
        private ProjectController controller;
        private Mock<IRepository<Project>> projectRepository;
        private Mock<IProjectService> projectService;
        private Mock<IRepository<Customer>> customerRepository;
        private int invalidProjectId = 11;
        private int validProjectId = 1;
        private BasicProject validProject;
        private BasicProjectViewModel projectViewModel;

        private Mock<IRepository<User>> userRepository;
        public ProjectControllerTests()
        {
            Mapper.AddProfile(new ProjectProfile());

            validProject = new BasicProject()
            {
                Id = 1,
                CustomerId = 1,
                EstimatedHours = 3.0M,
                Title = "New Project",
                ProjectManagerId = 1
            };

            projectViewModel = new BasicProjectViewModel() { UserName = "jdhall",
                Id=1,
                Title="Project View Model",
                CustomerId =1,
                EstimatedHours = 3.00M
                
            };

            projectService = new Mock<IProjectService>();
            projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(p => p.Find(invalidProjectId)).Returns((Project)null);
            projectRepository.Setup(p => p.Find(validProjectId)).Returns(validProject);
            customerRepository = new Mock<IRepository<Customer>>();
            userRepository = new Mock<IRepository<User>>();
            var users = new List<User> { new User() { Id = 1, UserName = "jdhall" } }.AsQueryable();
            userRepository.Setup(u => u.All).Returns(users);
            controller = new ProjectController(projectRepository.Object, projectService.Object, customerRepository.Object, userRepository.Object);

        }
        [TestMethod]
        public void RequestCreateNewBasicProjectReturnsCreateViewWithBasicViewModel()
        {
            var result = (ViewResult)controller.CreateProjectType(new BasicProjectViewModel());
            Assert.IsInstanceOfType(result.Model, typeof(BasicProjectViewModel));
        }

        [TestMethod]
        public void IndexGetsAllProjects()
        {            
            var result = (ViewResult)controller.Index();
            projectRepository.Verify(r => r.All, Times.Once(), "All projects was not accessed");            
        }

        [TestMethod]
        public void InvalidProjectIdDetailReturnsNotFound()
        {
            var result = controller.Details(invalidProjectId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void VaildProjectIdDetailReturnVaildProjectViewModel()
        {
            var result = (ViewResult)controller.Details(validProjectId);
            Assert.IsInstanceOfType(result.Model, typeof(BasicProjectViewModel));
        }

        [TestMethod]
        public void CreateProjectCallsProjectServiceAdd()
        {
            controller.Create(projectViewModel);
            projectService.Verify(p => p.Add(It.Is<Project>(pr => pr.Id ==projectViewModel.Id)));
        }

        [TestMethod]
        public void EditInvalidProjectIdReturnsNotFound()
        {
            var result = controller.Edit(invalidProjectId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditValidProjectIdReturnsViewResult()
        {
            var result = controller.Edit(validProjectId);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void EditCallsServiceEditFunctionOnProject()
        {
            var result = controller.Edit(projectViewModel);
            projectService.Verify(p => p.Edit(It.Is<Project>(pr => pr.Id == projectViewModel.Id)));
        }

        [TestMethod]
        public void DeleteInvalidProjectIdReturnsNotFound()
        {
            var result = controller.Delete(invalidProjectId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteValidProjectIdReturnsViewResult()
        {
            var result = controller.Delete(validProjectId);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteConfirmedInvalidProjectIdReturnsNotFound()
        {
            var result = controller.DeleteConfirmed(invalidProjectId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

       
        [TestMethod]
        public void DeleteConfirmedValidProjectIdCallsProjectServiceDeleteOnce()
        {
            var result = controller.DeleteConfirmed(validProjectId);
            projectService.Verify(p => p.Delete(validProject), Times.Once(), "Delete was not called once on project");
        }

        [TestMethod]
        public void CustomerViewBagSetupInCreateAction()
        {
            var result = (ViewResult)controller.CreateProjectType(projectViewModel);
            var selectList = (SelectList)result.ViewBag.Customers;
            var customers = selectList.Items;
            Assert.IsInstanceOfType(customers, typeof(List<Customer>));

        }
    }
}
