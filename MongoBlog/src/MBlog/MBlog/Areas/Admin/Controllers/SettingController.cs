using System.Web.Mvc;
using MBlog.Areas.Admin.Models;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MBlog.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        //
        // GET: /Admin/Setting/
        public ActionResult Index()
        {
            var collection = Database.GetCollection<Setting>("settings");
            var settings = collection.FindAll();
            return View(settings.MapTo<SettingViewModel>());
        }

        public ActionResult Create()
        {
            return View(new SettingViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SettingViewModel settingViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = Database.GetCollection<Setting>("settings");
                collection.Insert(settingViewModel.MapTo<Setting>());
                return RedirectToAction("Index");
            }
            return View(settingViewModel);
        }

        public ActionResult Edit(string id)
        {
            var collection = Database.GetCollection<Setting>("settings");
            var setting = collection.FindOneById(new BsonObjectId(id));
            return View(setting.MapTo<SettingViewModel>());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SettingViewModel settingViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = Database.GetCollection<Setting>("settings");
                var update = Update.Set("Value", settingViewModel.Value);
                update.Set("Name", settingViewModel.Name);
                collection.Update(Query.EQ("_id", new BsonObjectId(settingViewModel.SettingId)), update);
                return RedirectToAction("Index");
            }
            return View(settingViewModel);
        }

        public ActionResult Delete(string id)
        {
            var collection = Database.GetCollection<Setting>("settings");
            var setting = collection.FindOneById(new BsonObjectId(id));
            return View(setting.MapTo<SettingViewModel>());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            var collection = Database.GetCollection<Setting>("settings");
            collection.Remove(Query.EQ("_id", new BsonObjectId(id)));
            return RedirectToAction("Index");
        }
    }
}
