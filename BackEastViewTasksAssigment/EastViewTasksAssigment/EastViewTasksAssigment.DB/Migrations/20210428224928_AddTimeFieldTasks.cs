using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastViewTasksAssignment.DB.Migrations
{
    public partial class AddTimeFieldTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "taskTime",
                table: "Tasks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "taskTime",
                table: "Tasks");
        }
    }
}
