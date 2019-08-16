using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerDataBase.Models
{
    public class EventsDbContecst : DbContext
	{
		public EventsDbContecst(DbContextOptions<EventsDbContecst> options)
			: base(options)
		{ }

		public DbSet<Task> Tasks { get; set; }
	}
}
