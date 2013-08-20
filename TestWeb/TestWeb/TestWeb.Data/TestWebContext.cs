using System.Data.Entity;

namespace TestWeb.Data
{
    public class TestWebContext : DbContext
    {
        public DbSet<Example> Examples { get; set; }

        public TestWebContext() : base("name=TestDB")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Example>()
                        .HasKey(e => e.Id);
        }

    }

    public class Example
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}