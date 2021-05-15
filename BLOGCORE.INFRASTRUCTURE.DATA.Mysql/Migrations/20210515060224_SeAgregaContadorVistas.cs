using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.Mysql.Migrations
{
    public partial class SeAgregaContadorVistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VistasPagina",
                table: "Posts");

            migrationBuilder.AddColumn<long>(
                name: "VistasPaginaAnonimo",
                table: "Posts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VistasPaginaUsuario",
                table: "Posts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VistasPaginaAnonimo",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VistasPaginaUsuario",
                table: "Posts");

            migrationBuilder.AddColumn<long>(
                name: "VistasPagina",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
