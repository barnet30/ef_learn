using System;
using System.Data.Entity;

namespace teleprof
{
    public class teleprofContext : DbContext
    {
        public teleprofContext(): base("DbConnectionString")
        { }

        public DbSet<City> Cities { get; set; }
    }
}
