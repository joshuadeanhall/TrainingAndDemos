using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo4jExample.Models
{

    public class Employee
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<Employee> Manages { get; set; }
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
                if (levelsToBuild > 0)
                    employee.GetManages(levelsToBuild - 1);
                Manages.Add(employee);
            }
        }

    }
}