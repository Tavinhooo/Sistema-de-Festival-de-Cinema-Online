using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class TornarSessaoIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Sessoes_SessaoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "SessaoId",
                table: "Pedidos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Sessoes_SessaoId",
                table: "Pedidos",
                column: "SessaoId",
                principalTable: "Sessoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Sessoes_SessaoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "SessaoId",
                table: "Pedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Sessoes_SessaoId",
                table: "Pedidos",
                column: "SessaoId",
                principalTable: "Sessoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
