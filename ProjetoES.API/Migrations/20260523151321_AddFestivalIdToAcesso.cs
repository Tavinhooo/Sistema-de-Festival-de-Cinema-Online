using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFestivalIdToAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Acessos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Acessos");
        }
    }
}
