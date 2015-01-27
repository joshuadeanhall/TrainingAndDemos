using AutoMapper;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Statuos.Data;
using Statuos.Domain;
using Statuos.Service;
using Statuos.Web.Areas.Admin.Controllers;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Infrastructure.AutoMapper;
using Statuos.Web.Infrastructure.Installers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Statuos.Test.Controllers.Admin
{
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController controller;
        private Mock<IRepository<Customer>> customerRepositoryMock;
        private Mock<ICustomerService> customerServiceMock;
        CustomerViewModel customerVM;
        Customer customer;
        private int validCustomerId = 1;
        private int invalidCustomerId = 10;
        public CustomerControllerTests()
        {
            Setup();
            Mapper.CreateMap<Customer, CustomerViewModel>();
            Mapper.CreateMap<CustomerViewModel, Customer>();
        }
        [TestMethod]
        public void IndexReturnsListOfCustomers()
        {
            var result = (ViewResult)controller.Index();
            Assert.IsInstanceOfType(result.Model, typeof(List<CustomerViewModel>));
        }
        [TestMethod]
        public void DetailsReturnsNotWhenCustomerDoesntExist()
        {
            var result = controller.Details(invalidCustomerId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsReturnsViewWithCustomerViewModelForValidCustomerId()
        {
            var result = (ViewResult)controller.Details(validCustomerId);
            Assert.IsInstanceOfType(result.Model, typeof(CustomerViewModel));
        }

        [TestMethod]
        public void CreateCustomerViewModelCallsCustomerService()
        {
            var result = controller.Create(customerVM);
            customerServiceMock.Verify(c => c.Add(It.Is<Customer>(cu => cu.Id == customerVM.Id)));
        }

        [TestMethod]
        public void EditWithIdReturnsViewWithCustomerViewModel()
        {
            var result = (ViewResult)controller.Edit(validCustomerId);
            Assert.IsInstanceOfType(result.Model, typeof(CustomerViewModel));
        }

        [TestMethod]
        public void EditWithInvalidCustomerIdReturnsNotFoundView()
        {
            var result = controller.Edit(invalidCustomerId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }



        [TestMethod]
        public void CanViewCustomer()
        {
            int customerId = 1;
            controller.Details(customerId);
            customerRepositoryMock.Verify(r => r.Find(customerId), Times.Once(), "Find was not called");
        }

        [TestMethod]
        public void CanEditCustomer()
        {

            controller.Edit(customerVM);
            customerServiceMock.Verify(r => r.Edit(It.IsAny<Customer>()), Times.Once(), "Service call to edit was not made");
        }

        [TestMethod]
        public void DeleteWithInvalidIdReturnsNotFound()
        {
            var result = controller.Delete(invalidCustomerId);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteWithValidIdReturnsViewWithCustomerViewModel()
        {
            var result = (ViewResult)controller.Delete(validCustomerId);
            var customerViewModel = (CustomerViewModel)result.Model;
            Assert.AreEqual(validCustomerId, customerViewModel.Id);
        }
        [TestMethod]
        public void CanDeleteCustomerWithNoProjects()
        {
            int customerId = 1;
            Mock<IRepository<Customer>> customerRepositoryMock = new Mock<IRepository<Customer>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            var customer = new Customer()
            {
                Code = "TST",
                Id = customerId,
                Name = "Test",
                Projects = new List<Project>()
            };
            customerRepositoryMock.Setup(c => c.Find(customerId)).Returns(customer);
            var controller = new CustomerController(customerRepositoryMock.Object, customerServiceMock.Object);
            controller.DeleteConfirmed(customerVM.Id);
            customerServiceMock.Verify(r => r.Delete(It.IsAny<Customer>()), Times.Once(), "Service call to delete was not made");
        }

        [TestMethod]
        public void CantDeleteCustomerWithProjects()
        {
            int customerId = 1;
            Mock<IRepository<Customer>> customerRepositoryMock = new Mock<IRepository<Customer>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            var customer = new Customer()
            {
                Code = "TST",
                Id = customerId,
                Name = "Test",
                Projects = new List<Project>() { new BasicProject() { Id = 1, Title = "Project1", CustomerId = customerId, EstimatedHours = 2m}}
            };
            customerRepositoryMock.Setup(c => c.Find(customerId)).Returns(customer);
            var controller = new CustomerController(customerRepositoryMock.Object, customerServiceMock.Object);
            var result = (ViewResult)controller.DeleteConfirmed(customerVM.Id);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsInstanceOfType(result.Model, typeof(CustomerViewModel));
        }



        private void Setup()
        {
            customerVM = new CustomerViewModel() { Code = "TestCode", Name = "TestName", Id = validCustomerId };
            customer = new Customer() { Code = "TestCode", Name = "TestName", Id = validCustomerId };

            customerRepositoryMock = new Mock<IRepository<Customer>>();
            customerServiceMock = new Mock<ICustomerService>();
            controller = new CustomerController(customerRepositoryMock.Object, customerServiceMock.Object);
            var items = new List<Customer> { new Customer { Code = "testCode", Id = 1, Name = "testName" } }.AsQueryable();
            customerRepositoryMock.Setup(c => c.All).Returns(items);
            customerRepositoryMock.Setup(c => c.Find(invalidCustomerId)).Returns((Customer)null);
            customerRepositoryMock.Setup(c => c.Find(validCustomerId)).Returns(customer);
        }
    }
}
