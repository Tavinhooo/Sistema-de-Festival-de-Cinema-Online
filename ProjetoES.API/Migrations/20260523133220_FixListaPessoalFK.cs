using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class FixListaPessoalFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Utilizadores_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropIndex(
                name: "IX_ListaPessoais_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropColumn(
                name: "UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoais_MembroId",
                table: "ListaPessoais",
                column: "MembroId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPessoais_Utilizadores_MembroId",
                table: "ListaPessoais",
                column: "MembroId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Utilizadores_MembroId",
                table: "ListaPessoais");

            migrationBuilder.DropIndex(
                name: "IX_ListaPessoais_MembroId",
                table: "ListaPessoais");

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorId",
                table: "ListaPessoais",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoais_UtilizadorId",
                table: "ListaPessoais",
                column: "UtilizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPessoais_Utilizadores_UtilizadorId",
                table: "ListaPessoais",
                column: "UtilizadorId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
