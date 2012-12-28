using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Data.Configurations
{
    public class ChargeConfiguration : EntityTypeConfiguration<Charge>
    {
        public ChargeConfiguration()
        {
            HasKey(c => c.Id);
            this.HasRequired(c => c.User)
                .WithMany()
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(false);
            this.Property(c => c.Date).IsRequired();
        }
    }
}
