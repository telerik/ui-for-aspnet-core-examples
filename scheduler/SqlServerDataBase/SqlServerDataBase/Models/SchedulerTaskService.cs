using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerDataBase.Models
{
	public class SchedulerTaskService : ISchedulerEventService<TaskViewModel>
	{
		private EventsDbContecst db;

		public SchedulerTaskService(EventsDbContecst context)
		{
			db = context;
		}

		public virtual IQueryable<TaskViewModel> GetAll()
		{
			IQueryable<TaskViewModel> result = db.Tasks.Select(task => new TaskViewModel
			{
				TaskID = task.TaskID,
				Title = task.Title,
				Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
				End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
				StartTimezone = task.StartTimezone,
				EndTimezone = task.EndTimezone,
				Description = task.Description,
				IsAllDay = task.IsAllDay,
				RecurrenceRule = task.RecurrenceRule,
				RecurrenceException = task.RecurrenceException,
				RecurrenceID = task.RecurrenceID
			});

			return result;
		}

		public virtual void Insert(TaskViewModel task, ModelStateDictionary modelState)
		{
			if (ValidateModel(task, modelState))
			{

				if (string.IsNullOrEmpty(task.Title))
				{
					task.Title = "";
				}

				var entity = task.ToEntity();

				db.Tasks.Add(entity);
				db.SaveChanges();

				task.TaskID = entity.TaskID;
			}
		}

		public virtual void Update(TaskViewModel task, ModelStateDictionary modelState)
		{
			if (ValidateModel(task, modelState))
			{

				if (string.IsNullOrEmpty(task.Title))
				{
					task.Title = "";
				}

				var entity = task.ToEntity();
				db.Tasks.Attach(entity);
				db.Entry(entity).State = EntityState.Modified;
				db.SaveChanges();
			}
		}

		public virtual void Delete(TaskViewModel task, ModelStateDictionary modelState)
		{

			var entity = task.ToEntity();
			db.Tasks.Attach(entity);

			var recurrenceExceptions = db.Tasks.Where(t => t.RecurrenceID == task.TaskID);

			foreach (var recurrenceException in recurrenceExceptions)
			{
				db.Tasks.Remove(recurrenceException);
			}

			db.Tasks.Remove(entity);
			db.SaveChanges();
		}

		private bool ValidateModel(TaskViewModel appointment, ModelStateDictionary modelState)
		{
			if (appointment.Start > appointment.End)
			{
				modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
				return false;
			}

			return true;
		}

		public TaskViewModel One(Func<TaskViewModel, bool> predicate)
		{
			return GetAll().FirstOrDefault(predicate);
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
