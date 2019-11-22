using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Examples.Mvc.Models
{
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options)
            : base(options)
        { }

        public DbSet<EditorData> EditorData { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
