using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class ListaPessoalManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListaPessoalFilme",
                columns: table => new
                {
                    ListaPessoalId = table.Column<int>(type: "integer", nullable: false),
                    FilmeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPessoalFilme", x => new { x.ListaPessoalId, x.FilmeId });
                    table.ForeignKey(
                        name: "FK_ListaPessoalFilme_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaPessoalFilme_ListaPessoais_ListaPessoalId",
                        column: x => x.ListaPessoalId,
                        principalTable: "ListaPessoais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoalFilme_FilmeId",
                table: "ListaPessoalFilme",
                column: "FilmeId");

            migrationBuilder.Sql(@"INSERT INTO ""ListaPessoalFilme"" (""ListaPessoalId"", ""FilmeId"")
SELECT ""ListaPessoalId"", ""Id""
FROM ""Filmes""
WHERE ""ListaPessoalId"" IS NOT NULL;");

            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_ListaPessoais_ListaPessoalId",
                table: "Filmes");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_ListaPessoalId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "ListaPessoalId",
                table: "Filmes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaPessoalFilme");

            migrationBuilder.AddColumn<int>(
                name: "ListaPessoalId",
                table: "Filmes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_ListaPessoalId",
                table: "Filmes",
                column: "ListaPessoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_ListaPessoais_ListaPessoalId",
                table: "Filmes",
                column: "ListaPessoalId",
                principalTable: "ListaPessoais",
                principalColumn: "Id");
        }
    }
}
