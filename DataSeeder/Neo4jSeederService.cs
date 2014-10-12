using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace DataSeeder
{
    public class Neo4jSeederService
    {
        private GraphClient client;
        private int loopcounter = 0;
        public Neo4jSeederService()
        {
            var graphClientUri = ConfigurationManager.AppSettings["Neo4JEndpointUrl"];
            client = new GraphClient(new Uri(graphClientUri));
        }

        public void WriteNeo4JStuff(Employee employeeone)
        {
            client.Connect();
            var employee = GetSimpleEmployee(employeeone);
            client.Cypher.Create("(employee:Employee {newEmployee})")
                .WithParam("newEmployee", employee)
                .ExecuteWithoutResults();

            BuildManagesUsers(employeeone);
        }

        private void BuildManagesUsers(Employee bossEmployee)
        {
            foreach (var employee in bossEmployee.Manages)
            {
                loopcounter++;
                if (loopcounter > 1000)
                    break;
                var simpleEmployee = GetSimpleEmployee(employee);
                client.Cypher
                    .Match("(manager:Employee)")
                    .Where((SimpleEmployee manager) => manager.Id == bossEmployee.Id)
                    //.Create("manager-[:MANAGES]->(employee:Employee {simpleEmployee})")
                    .Create("(employee:Employee {simpleEmployee})-[:MANAGEDBY]->manager")
                    .WithParam("simpleEmployee", simpleEmployee)
                    .ExecuteWithoutResults();
                BuildManagesUsers(employee);
            }
        }

        private static SimpleEmployee GetSimpleEmployee(Employee employeeone)
        {
            var employee = new SimpleEmployee
            {
                Name = employeeone.Name,
                Id = employeeone.Id
            };
            return employee;
        }
    }

    public class SimpleEmployee
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
