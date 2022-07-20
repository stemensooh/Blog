using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class TablaRedesSociales2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilNavigationId",
                table: "RedesSociales");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociales_PerfilNavigationId",
                table: "RedesSociales");

            migrationBuilder.DropColumn(
                name: "PerfilNavigationId",
                table: "RedesSociales");

            migrationBuilder.AddColumn<long>(
                name: "PerfilId",
                table: "RedesSociales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_PerfilId",
                table: "RedesSociales",
                column: "PerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales",
                column: "PerfilId",
                principalTable: "Perfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociales_PerfilId",
                table: "RedesSociales");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "RedesSociales");

            migrationBuilder.AddColumn<long>(
                name: "PerfilNavigationId",
                table: "RedesSociales",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_PerfilNavigationId",
                table: "RedesSociales",
                column: "PerfilNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilNavigationId",
                table: "RedesSociales",
                column: "PerfilNavigationId",
                principalTable: "Perfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
