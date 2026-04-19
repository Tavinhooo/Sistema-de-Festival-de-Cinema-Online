using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaFestivais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Festival_FestivalId",
                table: "Filmes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Festival",
                table: "Festival");

            migrationBuilder.RenameTable(
                name: "Festival",
                newName: "Festivais");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Festivais",
                table: "Festivais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes",
                column: "FestivalId",
                principalTable: "Festivais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Festivais",
                table: "Festivais");

            migrationBuilder.RenameTable(
                name: "Festivais",
                newName: "Festival");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Festival",
                table: "Festival",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Festival_FestivalId",
                table: "Filmes",
                column: "FestivalId",
                principalTable: "Festival",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
