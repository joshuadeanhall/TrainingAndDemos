using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KO_Angular_Demo.Domain;
using KO_Angular_Demo.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KO_Angular_Demo.Api
{
    public class TasksController : ApiController
    {

        private readonly MongoDatabase _mongoDatabase;
        private readonly ApplicationUserManager _userManager;

        public TasksController(MongoDatabase mongoDatabase, ApplicationUserManager userManager)
        {
            if (mongoDatabase == null) throw new ArgumentNullException("mongoDatabase");
            _mongoDatabase = mongoDatabase;
            _userManager = userManager;
        }

        // GET: api/Tasks
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tasks/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tasks
        public void Post([FromBody]TaskViewModel taskViewModel)
        {
            var projects =
                _mongoDatabase.GetCollection<Project>("projects"); //.AsQueryable<Project>().FirstOrDefault(p => p.Id == new ObjectId(taskViewModel.ProjectId));
            var project =
                projects.AsQueryable().FirstOrDefault(p => p.Id == new ObjectId(taskViewModel.ProjectId));

            if (project != null)
            {
                if (project.Tasks == null)
                    project.Tasks = new List<ProjectTask>();
                project.Tasks.Add(new ProjectTask
                {
                    Effort = taskViewModel.Effort,
                    Name = taskViewModel.Name
                });
                projects.Save(project);
            }
        }

        // PUT: api/Tasks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tasks/5
        public void Delete(int id)
        {
        }
    }
}
