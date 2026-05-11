using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class FixSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaPessoalId",
                table: "Filmes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListaPessoais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    MembroId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPessoais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaPessoais_Visitantes_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Visitantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_ListaPessoalId",
                table: "Filmes",
                column: "ListaPessoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoais_MembroId",
                table: "ListaPessoais",
                column: "MembroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_ListaPessoais_ListaPessoalId",
                table: "Filmes",
                column: "ListaPessoalId",
                principalTable: "ListaPessoais",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_ListaPessoais_ListaPessoalId",
                table: "Filmes");

            migrationBuilder.DropTable(
                name: "ListaPessoais");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_ListaPessoalId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "ListaPessoalId",
                table: "Filmes");
        }
    }
}
