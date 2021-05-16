using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class TablaAccesoUsuarioYTablaVistasAnonimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VistasPaginaAnonimo",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VistasPaginaUsuario",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Vistas",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Usuarios",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Posts",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Perfiles",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccesoUsuarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(nullable: false),
                    Ip = table.Column<string>(maxLength: 20, nullable: true),
                    TipoAcceso = table.Column<int>(nullable: false),
                    DescripcionAcceso = table.Column<string>(maxLength: 500, nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    FechaAcceso = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VistasAnonimas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<long>(nullable: false),
                    FechaVista = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VistasAnonimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VistasAnonimas_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VistasAnonimas_PostId",
                table: "VistasAnonimas",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesoUsuarios");

            migrationBuilder.DropTable(
                name: "VistasAnonimas");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Vistas");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Perfiles");

            migrationBuilder.AddColumn<long>(
                name: "VistasPaginaAnonimo",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VistasPaginaUsuario",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
