using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class GeneralDbContext : IdentityDbContext
    {
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options)
            : base(options)
        { }

        public DbSet<EditorData> EditorData { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
