using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KO_Angular_Demo.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public string ProjectManager { get; set; }
        public int Effort { get; set; }
        public float Cost { get; set; }
    }
}
