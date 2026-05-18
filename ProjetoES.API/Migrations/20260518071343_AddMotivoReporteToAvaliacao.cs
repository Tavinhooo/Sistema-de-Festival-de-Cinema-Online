using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMotivoReporteToAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoReporte",
                table: "Avaliacoes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoReporte",
                table: "Avaliacoes");
        }
    }
}
