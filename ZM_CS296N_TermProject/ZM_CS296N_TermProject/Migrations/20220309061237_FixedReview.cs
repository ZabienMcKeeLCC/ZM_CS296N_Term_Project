using Microsoft.EntityFrameworkCore.Migrations;

namespace ZM_CS296N_TermProject.Migrations
{
    public partial class FixedReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "reviews",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "reviews");
        }
    }
}
