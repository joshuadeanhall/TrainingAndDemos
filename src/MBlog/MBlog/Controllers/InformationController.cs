using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using MBlog.Domain;
using MBlog.Models;
using MongoDB.Driver.Builders;

namespace MBlog.Controllers
{
    public class InformationController : MongoController
    {
        //
        // GET: /Information/
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contactViewModel)
        {
            var collection = Database.GetCollection<Setting>("settings");
            var toAddress = collection.FindOne(Query.EQ("Name", "Email"));
            var userName = ConfigurationManager.AppSettings["SMTP_Username"];
            var password = ConfigurationManager.AppSettings["SMTP_Password"];
            var smtpServer = ConfigurationManager.AppSettings["SMTP_Server"];
            var smtpPort = Int32.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
            var client = new SmtpClient(smtpServer, smtpPort) {Credentials = new NetworkCredential(userName, password)};
            var from = new MailAddress(contactViewModel.Email, contactViewModel.Name);
            var to = new MailAddress(toAddress.Value);

            var message = new MailMessage(from, to) {Body = contactViewModel.Message, Subject = "Blog contact page"};

            client.Send(message);

            return RedirectToAction("Index", "Blog");


        }

    }
}
