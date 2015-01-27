using System.Web.Mvc;
using System.Web.Security;
using MBlog.Domain;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MBlog.Models;

namespace MBlog.Controllers
{
    [Authorize]
    public class AccountController : MongoController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && LoginSuccessful(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private bool LoginSuccessful(string userName, string password, bool persistCookie)
        {
            var query = Query.EQ("UserName", BsonValue.Create(userName));
            var user = Database.GetCollection<User>("users").FindOne(query);

            if (user.Password != password) return false;
            FormsAuthentication.SetAuthCookie(userName, persistCookie);
            return true;
        }

        //
        // /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Blog");
        }
        

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Blog");
            }
        }
    }
}
