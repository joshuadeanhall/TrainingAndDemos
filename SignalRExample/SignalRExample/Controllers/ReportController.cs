using System.Web.Mvc;
using Messages;
using Rebus;
using SignalRExample.Models;

namespace SignalRExample.Controllers
{
    public class ReportController : Controller
    {
        public IBus Bus { get; set; }
        //
        // GET: /Report/
        public ActionResult Index()
        {
            return View(new ReportViewModel { ReportName = ""});
        }


        //
        // POST: /Report/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ReportViewModel model)
        {
            // TODO: Add insert logic here
            Bus.Send(new CreateReportMessage
            {
                ReportName = model.ReportName,
                UserName = User.Identity.Name
            });
            return RedirectToAction("Index");

        }
    }
}
