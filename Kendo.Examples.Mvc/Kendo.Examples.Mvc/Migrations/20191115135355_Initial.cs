using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kendo.Examples.Mvc.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditorData",
                columns: table => new
                {
                    ContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditorContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorData", x => x.ContentId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsAllDay = table.Column<bool>(nullable: false),
                    RecurrenceRule = table.Column<string>(nullable: true),
                    RecurrenceID = table.Column<int>(nullable: true),
                    RecurrenceException = table.Column<string>(nullable: true),
                    StartTimezone = table.Column<string>(nullable: true),
                    EndTimezone = table.Column<string>(nullable: true),
                    Task1TaskID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_Task1TaskID",
                        column: x => x.Task1TaskID,
                        principalTable: "Tasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Task1TaskID",
                table: "Tasks",
                column: "Task1TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditorData");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
