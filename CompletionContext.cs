using System.Data.Entity;

namespace Prom_IT
{
    class CompletionContext : DbContext
    {
        public CompletionContext() : base("DBConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompletionContext>());
        }
        public DbSet<Completion> Completions { get; set; }
        public void Clear()
        {
            // TODO : FIX hardcode table name here
            Database.ExecuteSqlCommand("TRUNCATE TABLE completions");
        }
    }
}
