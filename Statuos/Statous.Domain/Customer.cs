using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public class Customer
    {
        public virtual int Id { get; set; }

        [StringLength(25)]
        [MinLength(3)]
        public virtual string Name { get; set; }

        [StringLength(5)]
        public virtual string Code { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
