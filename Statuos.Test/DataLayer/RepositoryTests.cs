using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Statuos.Data;
using System.Data.Entity;
using Statuos.Domain;
using System.Linq;

namespace Statuos.Test.DataLayer
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<IUnitOfWork> uow;
        private Mock<IStatuosContext> context;
        IRepository<Customer> repository;
        public RepositoryTests()
        {
            Setup();
        }

        private void Setup()
        {
            uow = new Mock<IUnitOfWork>();
            context = new Mock<IStatuosContext>();
            uow.Setup(u => u.Context).Returns(context.Object);
            //context.Setup(c => c.Customers.Find()).Verifiable();
            repository = new Repository<Customer>(uow.Object);
        }
        [TestMethod]
        public void CanReturnAll()
        {
           
            var query = repository.All;
            context.Verify(c => c.Set<Customer>(), Times.Once(), "Call for all customers not made");
        }

        //[TestMethod]
        //public void CanFindById()
        //{
        //    //repository.Find(1);
        //    //context.Verify(c => c.Set<Customer>().Find(1), Times.Once(), "Find was not called properly");
        //}
    }
}
