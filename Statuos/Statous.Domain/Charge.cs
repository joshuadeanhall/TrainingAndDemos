using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public class Charge
    {
        public int Id { get; set; }
        public decimal Hours { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}
