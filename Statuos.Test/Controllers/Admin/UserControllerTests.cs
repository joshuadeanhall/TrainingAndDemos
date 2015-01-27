using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statuos.Web.Areas.Admin.Controllers;
using Moq;
using Statuos.Domain;
using Statuos.Data;
using Statuos.Service;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Statuos.Web.Models;
using System.Web.Mvc;

namespace Statuos.Test.Controllers.Admin
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller;
        private Mock<IRepository<User>> userRepository;
        private Mock<IUserService> userService;
        private int invalidUserId = 11;
        private int validUserId = 1;
        private User validUser;
        private UserViewModel userViewModel;
        public UserControllerTests()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();
            validUser = new User() { Id = validUserId, UserName = "jdhall", Projects = new List<Project>(), Tasks = new List<Task>()};
            userViewModel = (UserViewModel)Mapper.Map(validUser, validUser.GetType(), typeof(UserViewModel));
            userRepository = new Mock<IRepository<User>>();
            var users = new List<User> { new User() { Id = validUserId, UserName = "jdhall" } }.AsQueryable();
            userRepository.Setup(u => u.All).Returns(users);
            userRepository.Setup(u => u.Find(invalidUserId)).Returns((User)null);
            userRepository.Setup(u => u.Find(validUserId)).Returns(validUser);
            userService = new Mock<IUserService>();
            controller = new UserController(userRepository.Object, userService.Object);

        }
        [TestMethod]
        public void IndexCallsAllOnUserRepository()
        {
            var result = controller.Index();
            userRepository.Verify(u => u.All, Times.Once(), "All users was not called");
        }

        [TestMethod]
        public void DetailReturnsNotFoundForInvalidId()
        {
            var result = controller.Details(invalidUserId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailReturnsUserViewModelForValidId()
        {
            var result = (ViewResult)controller.Details(validUserId);
            var userViewModel = (UserViewModel)result.Model;
            Assert.AreEqual(validUserId, userViewModel.Id);
        }

        [TestMethod]
        public void CreateCallsServiceAddMethodWithUser()
        {
            var result = controller.Create(userViewModel);
            userService.Verify(us => us.Add(It.Is<User>(u => u.Id == userViewModel.Id)), Times.Once(), "Add was not called once");
        }

        [TestMethod]
        public void EditReturnsNotFoundForInvalidId()
        {
            var result = controller.Edit(invalidUserId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditReturnsValiUserViewModelForValidId()
        {
            var result = (ViewResult)controller.Edit(validUserId);
            var userVM = (UserViewModel)result.Model;
            Assert.AreEqual(validUserId, userVM.Id);
        }

        [TestMethod]
        public void DeleteReturnsNotFoundForInvalidId()
        {
            var result = controller.Delete(invalidUserId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteRetunsValidUserModelForValidId()
        {
            var result = (ViewResult)controller.Delete(validUserId);
            var model = (UserViewModel)result.Model;

            Assert.AreEqual(validUserId, model.Id);
        }

        [TestMethod]
        public void DeleteConfirmReturnsNotFoundForInvalidId()
        {
            var result = controller.DeleteConfirmed(invalidUserId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteConfirmCallsUserServiceDeleteForValidId()
        {
            var result = controller.DeleteConfirmed(validUserId);
            userService.Verify(us => us.Delete(It.Is<User>(u => u.Id == validUserId)));
            
        }


    }
}
