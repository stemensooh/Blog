using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Migrations
{
    public partial class CambioDefinicionVistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VistasPaginaUsuario",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VistasPaginaUsuario",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
