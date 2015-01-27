using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Statuos.Data
{
    public interface IStatuosContext : IDisposable
    {
        DbSet<Customer> Customers { get; }
        DbSet<Project> Projects { get; }
        DbSet<User> Users { get; }
        DbSet<Task> Tasks { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        int Save();
    }
}
