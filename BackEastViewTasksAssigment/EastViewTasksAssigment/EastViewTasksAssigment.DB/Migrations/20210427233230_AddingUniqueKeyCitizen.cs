using Microsoft.EntityFrameworkCore.Migrations;

namespace EastViewTasksAssigment.DB.Migrations
{
    public partial class AddingUniqueKeyCitizen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Citizens_citizenIdCard",
                table: "Citizens",
                column: "citizenIdCard",
                unique: true,
                filter: "[citizenIdCard] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Citizens_citizenIdCard",
                table: "Citizens");
        }
    }
}
