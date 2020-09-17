using System.Data.Entity;

namespace Prom_IT
{
    class CompletionContext : DbContext
    {
        public CompletionContext() : base("DBConnection") { }
        public DbSet<Completion> Completions { get; set; }
        public void Clear()
        {
            // TODO : FIX hardcode table name here
            // TODO : handle exception here
            Database.ExecuteSqlCommand("TRUNCATE TABLE completions");
        }
    }
}
