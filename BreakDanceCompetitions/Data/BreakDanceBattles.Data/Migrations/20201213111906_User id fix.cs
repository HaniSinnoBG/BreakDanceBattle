using Microsoft.EntityFrameworkCore.Migrations;

namespace BreakDanceBattles.Data.Migrations
{
    public partial class Useridfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionUsers_AspNetUsers_UserId1",
                table: "CompetitionUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionUsers_UserId1",
                table: "CompetitionUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CompetitionUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CompetitionUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionUsers_UserId",
                table: "CompetitionUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionUsers_AspNetUsers_UserId",
                table: "CompetitionUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionUsers_AspNetUsers_UserId",
                table: "CompetitionUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionUsers_UserId",
                table: "CompetitionUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CompetitionUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CompetitionUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionUsers_UserId1",
                table: "CompetitionUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionUsers_AspNetUsers_UserId1",
                table: "CompetitionUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
