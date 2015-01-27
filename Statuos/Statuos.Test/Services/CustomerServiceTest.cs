using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Statuos.Domain;
using Statuos.Service;
using Statuos.Data;

namespace Statuos.Test.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        private CustomerService service;
        private Mock<IUnitOfWork> uow;
        private Mock<IRepository<Customer>> repository;
        private Customer customer;
        public CustomerServiceTest()
        {
            Setup();
        }

        private void Setup()
        {
            uow = new Mock<IUnitOfWork>();
            repository = new Mock<IRepository<Customer>>();
            service = new CustomerService(repository.Object, uow.Object);
            customer = new Customer() { Code = "TestCode", Name = "TestName", Id = 1 };
        }
        [TestMethod]
        public void CanAddCustomer()
        {
            service.Add(customer);
            repository.Verify(r => r.Add(It.IsAny<Customer>()), Times.Once(), "One Customer was not added");
            uow.Verify(u => u.Save(), Times.Once(), "Customer was not saved");
        }

        [TestMethod]
        public void CanEditCustomer()
        {
            service.Edit(customer);
            repository.Verify(r => r.Edit(It.IsAny<Customer>()), Times.Once(), "Customer not edited in repository");
            uow.Verify(u => u.Save(), Times.Once(), "Customer was not saved");
        }

        [TestMethod]
        public void CanDeleteCustomer()
        {
            service.Delete(customer);
            repository.Verify(r => r.Delete(customer), Times.Once(), "Customer not deleted from repository");
            uow.Verify(u => u.Save(), Times.Once(), "Customer changes not saved");
        }
    }
}
