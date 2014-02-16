using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messages;
using Rebus;

namespace SignalRExample.Controllers
{
    public class MessageController : Controller
    {
        public IBus Bus { get; set; }
        //
        // GET: /Message/
        public ActionResult Index()
        {
            var message = new NotificationMessage();
            message.Message = "test1234";
            Bus.Send(message);
            return View();
        }

        //
        // GET: /Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Message/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Message/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
