using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class TablaRedesSociales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RedesSociales",
                columns: table => new
                {
                    RedesSocialesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Icono = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    PerfilNavigationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociales", x => x.RedesSocialesId);
                    table.ForeignKey(
                        name: "FK_RedesSociales_Perfiles_PerfilNavigationId",
                        column: x => x.PerfilNavigationId,
                        principalTable: "Perfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_PerfilNavigationId",
                table: "RedesSociales",
                column: "PerfilNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedesSociales");
        }
    }
}
