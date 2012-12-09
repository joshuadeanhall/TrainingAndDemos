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
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Infrastructure.AutoMapper;

namespace Statuos.Web.Areas.Admin.Controllers
{
    public class CustomerController : AdminController
    {
        private IRepository<Customer> _repository;
        private ICustomerService _service;

        public CustomerController(IRepository<Customer> repository, ICustomerService customerService)
        {            
            _repository = repository;
            _service = customerService;
        }

        //
        // GET: /Admin/Customer/

        public ActionResult Index()
        {
            var customers = _repository.All.ToList();
            var customerVMs = customers.MapTo<CustomerViewModel>();
            return View(customerVMs);
        }

        //
        // GET: /Admin/Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            Customer customer = _repository.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer.MapTo<CustomerViewModel>());
        }

        //
        // GET: /Admin/Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _service.Add(customer.MapTo<Customer>());
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Admin/Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Customer customer =_repository.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer.MapTo<CustomerViewModel>());
        }

        //
        // POST: /Admin/Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _service.Edit(customer.MapTo<Customer>());
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Admin/Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customer customer = _repository.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer.MapTo<CustomerViewModel>());
        }

        //
        // POST: /Admin/Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _repository.Find(id);
            if (customer.Projects.Count > 0)
            {
                ModelState.AddModelError("", "This customer has projects associated with it and can not be deleted");
            }
            if (ModelState.IsValid)
            {
                _service.Delete(customer);
                return RedirectToAction("Index");
            }
            return View(customer.MapTo<CustomerViewModel>());
        }

        protected override void Dispose(bool disposing)
        {
            //TODO fix dispose here
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}