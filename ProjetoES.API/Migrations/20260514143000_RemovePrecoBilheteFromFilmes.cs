using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    public partial class RemovePrecoBilheteFromFilmes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoBilhete",
                table: "Filmes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoBilhete",
                table: "Filmes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}