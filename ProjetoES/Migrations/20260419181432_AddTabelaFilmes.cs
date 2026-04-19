using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaFilmes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Filmes",
                newName: "Sinopse");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Filmes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMinutos",
                table: "Filmes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Filmes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoBilhete",
                table: "Filmes",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "DuracaoMinutos",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "PrecoBilhete",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "Sinopse",
                table: "Filmes",
                newName: "Descricao");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Filmes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
