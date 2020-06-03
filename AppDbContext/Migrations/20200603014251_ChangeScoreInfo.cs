using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class ChangeScoreInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Teacher_TeacherId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_TeacherId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Score");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Score",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_CourseId",
                table: "Score",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Course_CourseId",
                table: "Score",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Course_CourseId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_CourseId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Score");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Score",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_TeacherId",
                table: "Score",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Teacher_TeacherId",
                table: "Score",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
