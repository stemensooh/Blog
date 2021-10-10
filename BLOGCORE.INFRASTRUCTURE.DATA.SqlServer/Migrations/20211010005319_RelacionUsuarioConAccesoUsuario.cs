using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class RelacionUsuarioConAccesoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccesoUsuarios_UsuarioId",
                table: "AccesoUsuarios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccesoUsuarios_Usuarios_UsuarioId",
                table: "AccesoUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_AccesoUsuarios_UsuarioId",
                table: "AccesoUsuarios");
        }
    }
}
