using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Database
{
    public class InMemoryDbContext: DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
           : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }

        public DbSet<EmployeeViewModel> Employees { get; set; }
    }
}
