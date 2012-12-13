using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Statuos.Data
{
    public class StatuosContext : DbContext, IStatuosContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public StatuosContext()
            : base("name=StatuosDB")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasicProject>().ToTable("BasicProject");
            modelBuilder.Entity<MaxHoursProject>().ToTable("MaxHoursProject");
            modelBuilder.Entity<BasicTask>().ToTable("BasicTask");
            modelBuilder.Entity<PhoneRequestTask>().ToTable("PhoneRequestTask");

            
            modelBuilder.Entity<Task>()                
            .HasMany(t => t.Users)            
            .WithMany(u => u.Tasks)            
            .Map(m => m.MapLeftKey("TaskId")
                   .MapRightKey("UserId")
                   .ToTable("TaskUser"));

            modelBuilder.Entity<Task>()
                .HasRequired(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired();
            //Default value for isactive

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithRequired(p => p.ProjectManager)
                .HasForeignKey(p => p.ProjectManagerId);

               
               

        }

        public int Save()
        {            
            return this.SaveChanges();            
        }

        public void Dispose()
        {
            base.Dispose();
        }

    }
}
