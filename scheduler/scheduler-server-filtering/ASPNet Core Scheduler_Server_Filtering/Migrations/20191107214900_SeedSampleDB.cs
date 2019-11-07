using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SqlServerDataBase.Migrations
{
    public partial class SeedSampleDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Tasks",
               columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
               values: new object[] { 1, "Custom event description", DateTime.Today.AddDays(-5).AddHours(12), null, false, null, null, null, DateTime.Today.AddDays(-5).AddHours(11), null, null, "Discuss with Robert the mail" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 2, "Custom event description", DateTime.Today.AddDays(-6).AddHours(14), null, false, null, null, null, DateTime.Today.AddDays(-6).AddHours(13), null, null, "Lunch with John" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 3, "Custom event description", DateTime.Today.AddDays(-6).AddHours(15), null, false, null, null, null, DateTime.Today.AddDays(-6).AddHours(13).AddMinutes(30), null, null, "Call with Martin" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 4, "Custom event description", DateTime.Today.AddDays(-7).AddHours(9).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(-7).AddHours(8), null, null, "Yoga training" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 5, "Custom event description", DateTime.Today.AddDays(-4).AddHours(11), null, false, null, null, null, DateTime.Today.AddDays(-4).AddHours(10), null, null, "Meet & Greet the visitors of the 'Open Days' event" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 6, "Custom event description", DateTime.Today.AddDays(-2).AddHours(12), null, false, null, null, null, DateTime.Today.AddDays(-2).AddHours(11), null, null, "'Teams' meeting" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 7, "Custom event description", DateTime.Today.AddDays(-1).AddHours(13).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(-1).AddHours(12), null, null, "Team lunch" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 8, "Custom event description", DateTime.Today.AddHours(10), null, false, null, null, null, DateTime.Today.AddHours(9), null, null, "Tennis training" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 9, "Custom event description", DateTime.Today.AddHours(16), null, false, null, null, null, DateTime.Today.AddHours(14).AddMinutes(30), null, null, "Call Charlie about the project" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 10, "Custom event description", DateTime.Today.AddDays(1).AddHours(8), null, false, null, null, null, DateTime.Today.AddDays(1).AddHours(7), null, null, "Bring the kids to school" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 11, "Custom event description", DateTime.Today.AddDays(1).AddHours(12), null, false, null, null, null, DateTime.Today.AddDays(1).AddHours(11), null, null, "Mothly retrospective meeting" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 12, "Custom event description", DateTime.Today.AddDays(2).AddHours(19), null, false, null, null, null, DateTime.Today.AddDays(2).AddHours(16).AddMinutes(30), null, null, "Bowling tournament" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 13, "Custom event description", DateTime.Today.AddDays(4).AddHours(10), null, false, null, null, null, DateTime.Today.AddDays(4).AddHours(9), null, null, "Package delivery" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 14, "Custom event description", DateTime.Today.AddDays(5).AddHours(15), null, false, null, null, null, DateTime.Today.AddDays(5).AddHours(14), null, null, "OKRs review" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 15, "Custom event description", DateTime.Today.AddDays(6).AddHours(10).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(6).AddHours(8), null, null, "DevReach preparation meeting" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 16, "Custom event description", DateTime.Today.AddDays(7).AddHours(14), null, false, null, null, null, DateTime.Today.AddDays(7).AddHours(12), null, null, "Lunch with Maria" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 17, "Custom event description", DateTime.Today.AddDays(8).AddHours(12).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(8).AddHours(7), null, null, "Breakfast @ Starbucks" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 18, "Custom event description", DateTime.Today.AddDays(9).AddHours(15).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(9).AddHours(12), null, null, "Take the Dog to the vet" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 19, "Custom event description", DateTime.Today.AddDays(-8).AddHours(12).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(-8).AddHours(11), null, null, "Meet the babysitter" });
            
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskID", "Description", "End", "EndTimezone", "IsAllDay", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "Start", "StartTimezone", "Task1TaskID", "Title" },
                values: new object[] { 20, "Custom event description", DateTime.Today.AddDays(-9).AddHours(10).AddMinutes(30), null, false, null, null, null, DateTime.Today.AddDays(-9).AddHours(9), null, null, "Morning Yoga retreat" });

        }

    }
}
