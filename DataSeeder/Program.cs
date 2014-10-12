using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents;
using Neo4jClient;

namespace DataSeeder
{
    class Program
    {
        private static DocumentClient client;
        static void Main(string[] args)
        {
            var employeeone = new Employee
            {
                Name = "BossMan",
                Email = "BossMan@company",
                Id = Guid.NewGuid()
            };

            var dev1 = new Employee
            {
                Name = "DEV1",
                Email = "Dev1",
                Id = Guid.NewGuid()
            };

            var adm1 = new Employee
            {
                Name = "ADM1",
                Email = "ADM1@compnany.com",
                Id = Guid.NewGuid()
            };

            var stl1 = new Employee
            {
                Name = "stl1",
                Email = "stl1",
                Id = Guid.NewGuid()
            };

            var josh = new Employee
            {
                Name = "Josh",
                Email = "Josh",
                Id = Guid.NewGuid()
            };

            var sd2 = new Employee
            {
                Name = "SD2",
                Email = "SD2@gmai",
                Id = Guid.NewGuid()
            };

            stl1.Manages.Add(josh);
            stl1.Manages.Add(sd2);
            adm1.Manages.Add(dev1);
            adm1.Manages.Add(stl1);

            var adm2 = new Employee
            {
                Name = "adm2",
                Email = "adm2",
                Id = Guid.NewGuid()
            };

            var dev2 = new Employee
            {
                Name = "dev2",
                Email = "dev2",
                Id = Guid.NewGuid()
            };

            var adm3 = new Employee
            {
                Name = "adm3",
                Email = "adm3",
                Id = Guid.NewGuid()
            };

            var stl2 = new Employee
            {
                Name = "stl2",
                Email = "stl2",
                Id = Guid.NewGuid()
            };


            adm2.Manages.Add(dev2);
            adm3.Manages.Add(stl2);

            var vp = new Employee()
            {
                Name = "SJ",
                Email = "SJ@company.com",
                Id = Guid.NewGuid()
                
            };

            vp.Manages.Add(adm1);
            vp.Manages.Add(adm2);
            vp.Manages.Add(adm3);



            employeeone.Manages.Add(vp);

            //employeeone.GetManages(2);


            var endpointUrl = ConfigurationManager.AppSettings["DocumentDbEndpointUrl"];
            var authorizationKey = ConfigurationManager.AppSettings["DocumentDbAuthorizationKey"];
            using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                //RunAsync(employeeone).Wait();
            }

            var neoService = new Neo4jSeederService();
            neoService.WriteNeo4JStuff(employeeone);
        }


        private static async Task RunAsync(Employee employee)
        {
            // Try to retrieve a Database if exists, else create the Database
            var database = await RetrieveOrCreateDatabaseAsync("EmployeesRegistry");

            // Try to retrieve a Document Collection, else create the Document Collection
            var collection = await RetrieveOrCreateCollectionAsync(database.SelfLink, "EmployeesCollection");

            // Create two documents within the recently created or retrieved Game collection
            //await CreateGameDocumentsAsync(collection.SelfLink);
            await client.CreateDocumentAsync(collection.SelfLink, employee);

            
            // Use DocumentDB SQL to query the documents within the Game collection
            var employees = client.CreateDocumentQuery(collection.SelfLink, "SELECT * FROM EmployeesCollection").ToArray().FirstOrDefault();

        }


        private static async Task<Database> RetrieveOrCreateDatabaseAsync(string id)
        {
            // Try to retrieve the database (Microsoft.Azure.Documents.Database) whose Id is equal to databaseId            
            var database = client.CreateDatabaseQuery().Where(db => db.Id == id).AsEnumerable().FirstOrDefault();

            // If the previous call didn't return a Database, it is necessary to create it
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = id });
                Console.WriteLine("Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            }

            return database;
        }

        private static async Task<DocumentCollection> RetrieveOrCreateCollectionAsync(string databaseSelfLink, string id)
        {
            // Try to retrieve the collection (Microsoft.Azure.Documents.DocumentCollection) whose Id is equal to collectionId
            var collection = client.CreateDocumentCollectionQuery(databaseSelfLink).Where(c => c.Id == id).ToArray().FirstOrDefault();

            // If the previous call didn't return a Collection, it is necessary to create it
            if (collection != null)
            {
                await client.DeleteDocumentCollectionAsync(collection.SelfLink);
            }
            collection = await client.CreateDocumentCollectionAsync(databaseSelfLink, new DocumentCollection { Id = id });
            return collection;
        }

    }



    public class Employee
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<Employee> Manages  { get; set; }
        private const int ManagesConst = 7;

        public Employee()
        {
            Manages = new List<Employee>();
        }

        public void GetManages(int levelsToBuild)
        {
            int totalManaged = ManagesConst - levelsToBuild;
            for (int i = 0; i < totalManaged; i++)
            {
                var employee = new Employee
                {
                    Name = string.Format("Employee{0}Level{1}", i, levelsToBuild),
                    Email = string.Format("Employee{0}Level{1}@company.com", i, levelsToBuild),
                    Id = Guid.NewGuid()
                };
                if(levelsToBuild > 0)
                    employee.GetManages(levelsToBuild - 1);
                Manages.Add(employee);
            }
        }
    }
}
