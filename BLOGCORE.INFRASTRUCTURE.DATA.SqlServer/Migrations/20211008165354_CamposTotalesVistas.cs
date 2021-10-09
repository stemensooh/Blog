using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class CamposTotalesVistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalVistas",
                table: "Posts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalVistasAnonimas",
                table: "Posts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVistas",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TotalVistasAnonimas",
                table: "Posts");
        }
    }
}
