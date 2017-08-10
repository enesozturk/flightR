using Model;
using System.Data.Entity;

namespace Data
{
    public class Context : DbContext
    {
        public Context()
            : base("name=flighREntities")
        {
            //Database.SetInitializer<DbContext>(
            //    new CreateDatabaseIfNotExists<DbContext>());
        }
        public virtual DbSet<Record> record { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
