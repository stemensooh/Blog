using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class AltColComentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Comentarios",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Comentarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Comentarios");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Comentarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
