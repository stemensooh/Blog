using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class TablaRedesSociales3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales");

            migrationBuilder.AlterColumn<long>(
                name: "PerfilId",
                table: "RedesSociales",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "RedesSociales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TipoRedSocial",
                columns: table => new
                {
                    TipoRedSocialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Icono = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRedSocial", x => x.TipoRedSocialId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales",
                column: "PerfilId",
                principalTable: "Perfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales");

            migrationBuilder.DropTable(
                name: "TipoRedSocial");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "RedesSociales");

            migrationBuilder.AlterColumn<long>(
                name: "PerfilId",
                table: "RedesSociales",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Perfiles_PerfilId",
                table: "RedesSociales",
                column: "PerfilId",
                principalTable: "Perfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
