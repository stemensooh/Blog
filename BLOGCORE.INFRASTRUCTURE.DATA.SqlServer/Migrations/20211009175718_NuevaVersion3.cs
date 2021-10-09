using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class NuevaVersion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_ComentarioId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_ComentarioId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ComentarioId",
                table: "Comentarios");

            migrationBuilder.AddColumn<long>(
                name: "ComentarioPadreId",
                table: "Comentarios",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ComentarioPadreId",
                table: "Comentarios");

            migrationBuilder.AddColumn<long>(
                name: "ComentarioId",
                table: "Comentarios",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ComentarioId",
                table: "Comentarios",
                column: "ComentarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_ComentarioId",
                table: "Comentarios",
                column: "ComentarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
