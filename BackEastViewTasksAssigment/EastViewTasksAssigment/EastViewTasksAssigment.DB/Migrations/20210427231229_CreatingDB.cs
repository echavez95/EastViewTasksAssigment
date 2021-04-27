using Microsoft.EntityFrameworkCore.Migrations;

namespace EastViewTasksAssigment.DB.Migrations
{
    public partial class CreatingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    citizenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    citizenIdCard = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    citizenName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    citizenLastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.citizenId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    taskId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskDay = table.Column<byte>(type: "tinyint", nullable: false),
                    taskDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    taskCitizenId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.taskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Citizens_taskCitizenId",
                        column: x => x.taskCitizenId,
                        principalTable: "Citizens",
                        principalColumn: "citizenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_taskCitizenId",
                table: "Tasks",
                column: "taskCitizenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Citizens");
        }
    }
}
