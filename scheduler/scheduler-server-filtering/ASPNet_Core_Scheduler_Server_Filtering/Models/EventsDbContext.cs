using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerDataBase.Models
{
    public class EventsDbContext : DbContext
	{
		public EventsDbContext(DbContextOptions<EventsDbContext> options)
			: base(options)
		{ }

		public DbSet<Task> Tasks { get; set; }
	}
}
