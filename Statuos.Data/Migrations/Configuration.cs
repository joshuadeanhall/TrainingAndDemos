namespace Statuos.Data.Migrations
{
    using Statuos.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StatuosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StatuosContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var user1 = new User { Id = 1, UserName = @"HOME\jdhall" };
            List<User> users = new List<User>();
            users.Add(user1);
            context.Users.AddOrUpdate(
                user1,
                new User { Id = 2, UserName = "imosley" }
                    );


            context.Customers.AddOrUpdate(
                new Customer { Id=1, Code = "MIC", Name = "Microsoft" },
                new Customer { Id=2,  Code = "Hos", Name = "Hostees" }
                );
            context.Projects.AddOrUpdate(
                new BasicProject {Id=1, EstimatedHours = 2, Title = "PRoject 1", CustomerId = 1, ProjectManagerId = 1 },
                new MaxHoursProject {Id=2, MaxHours = 1.2M, Title = "Project2", EstimatedHours = 1, CustomerId = 2, ProjectManagerId = 1 }
                    );
            context.Tasks.Add(new BasicTask() { Id = 1, ProjectId = 1, Title = "Task 1", EstimatedHours = 1, Users = users });
        }
    }
}
