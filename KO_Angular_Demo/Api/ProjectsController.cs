using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KO_Angular_Demo.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KO_Angular_Demo.Api
{
    public class ProjectsController : ApiController
    {
        private readonly MongoDatabase _mongoDatabase;

        public ProjectsController(MongoDatabase mongoDatabase)
        {
            if (mongoDatabase == null) throw new ArgumentNullException("mongoDatabase");
            _mongoDatabase = mongoDatabase;
        }

        // GET: api/Project
        public IEnumerable<Project> Get()
        {
            var collection = _mongoDatabase.GetCollection<Project>("collectionname");
            return collection.AsQueryable().Where(c => c.ProjectManager == User.Identity.Name).ToList();
        }

        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Project
        public void Post([FromBody]string value)
        {
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
