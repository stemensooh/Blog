using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class AccesoUsuarioNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "AccesoUsuarios",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "AccesoUsuarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
