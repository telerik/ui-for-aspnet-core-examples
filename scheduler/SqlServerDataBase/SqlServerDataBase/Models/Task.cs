using System;
using System.Collections.Generic;

namespace SqlServerDataBase.Models
{
    public class Task
    {
		public Task()
		{
			this.Tasks1 = new HashSet<Task>();
		}

		public int TaskID { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsAllDay { get; set; }
		public string RecurrenceRule { get; set; }
		public Nullable<int> RecurrenceID { get; set; }
		public string RecurrenceException { get; set; }
		public string StartTimezone { get; set; }
		public string EndTimezone { get; set; }

		public virtual ICollection<Task> Tasks1 { get; set; }
		public virtual Task Task1 { get; set; }
	}
}
