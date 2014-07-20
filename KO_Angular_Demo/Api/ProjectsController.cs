using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KO_Angular_Demo.Domain;
using KO_Angular_Demo.Infrastructure.Automapper;
using KO_Angular_Demo.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KO_Angular_Demo.Api
{
    public class ProjectsController : ApiController
    {
        private readonly MongoDatabase _mongoDatabase;
        private readonly ApplicationUserManager _userManager;

        public ProjectsController(MongoDatabase mongoDatabase, ApplicationUserManager userManager)
        {
            if (mongoDatabase == null) throw new ArgumentNullException("mongoDatabase");
            _mongoDatabase = mongoDatabase;
            _userManager = userManager;
        }

        // GET: api/Project
        public IEnumerable<ProjectViewModel> Get()
        {
            var collection = _mongoDatabase.GetCollection<Project>("projects");
            var user = _userManager.FindById(User.Identity.GetUserId());
            var projects = collection.AsQueryable().Where(c => c.ProjectManager == user).ToList();
            return projects.MapTo<ProjectViewModel>();
        }

        // GET: api/Project/5
        public ProjectViewModel Get(string id)
        {
            var project = _mongoDatabase.GetCollection<Project>("projects").AsQueryable<Project>().FirstOrDefault(p => p.Id == new ObjectId(id));
            var projectViewModel = project.MapTo<ProjectViewModel>();
            return projectViewModel;
        }

        // POST: api/Project
        public void Post([FromBody]Project value)
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            value.ProjectManager = user;
            _mongoDatabase.GetCollection<Project>("projects").Insert(value);
        }

        // PUT: api/Project/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
        }
    }
}
