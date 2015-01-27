using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
