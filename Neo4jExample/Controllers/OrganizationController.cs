using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Neo4jClient;
using Neo4jExample.Models;

namespace Neo4jExample.Controllers
{
    public class OrganizationController : Controller
    {
        
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetManagementByName()
        {
                return View(new List<string>());
        }

        [HttpPost]
        public ActionResult GetManagementByNameSubmit(string name)
        {
            var graphClientUri = ConfigurationManager.AppSettings["Neo4JEndpointUrl"];
            var client = new GraphClient(new Uri(graphClientUri));
            client.Connect();

            var results = client.Cypher.Match("(baseEmployee:Employee)-[:MANAGEDBY*0..]->(manager)")
                .Where((Employee baseEmployee) => baseEmployee.Name == name)
                .Return(manager => manager.As<Employee>().Name).Results;


            results = results.Reverse();
            //client.
            return View("GetManagementByName", results);
        }

        public ActionResult ObjectJson()
        {
            string documentAsJson = "";
            var endpointUrl = ConfigurationManager.AppSettings["DocumentDbEndpointUrl"];
            var authorizationKey = ConfigurationManager.AppSettings["DocumentDbAuthorizationKey"];
            using ( var client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                var database = client.CreateDatabaseQuery().Where(db => db.Id == "EmployeesRegistry").AsEnumerable().FirstOrDefault();
                var collection = client.CreateDocumentCollectionQuery(database.SelfLink).Where(c => c.Id == "EmployeesCollection").ToArray().FirstOrDefault();
                documentAsJson = client.CreateDocumentQuery(collection.SelfLink, "SELECT * FROM EmployeesCollection").ToArray().FirstOrDefault().ToString();
            }
            return View(model: documentAsJson);
        }
    }
}