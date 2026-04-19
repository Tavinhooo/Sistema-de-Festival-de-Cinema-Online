using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoES.Migrations
{
    /// <inheritdoc />
    public partial class AddFestivaisERelacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Filmes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PosterUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes",
                column: "FestivalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Festival_FestivalId",
                table: "Filmes",
                column: "FestivalId",
                principalTable: "Festival",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Festival_FestivalId",
                table: "Filmes");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Filmes");
        }
    }
}
