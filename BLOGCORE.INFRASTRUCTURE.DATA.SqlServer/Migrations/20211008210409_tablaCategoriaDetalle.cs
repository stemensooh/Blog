using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGCORE.INFRASTRUCTURE.DATA.SqlServer.Migrations
{
    public partial class tablaCategoriaDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorias_CategoriaId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoriaId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "CategoriasPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(nullable: false),
                    PostId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasPosts_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoriasPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasPosts_CategoriaId",
                table: "CategoriasPosts",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasPosts_PostId",
                table: "CategoriasPosts",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasPosts");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoriaId",
                table: "Posts",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorias_CategoriaId",
                table: "Posts",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
