using Microsoft.EntityFrameworkCore.Migrations;

namespace EastViewTasksAssignment.DB.Migrations
{
    public partial class AddingFieldCitizenAlive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "citizenAlive",
                table: "Citizens",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "citizenAlive",
                table: "Citizens");
        }
    }
}
