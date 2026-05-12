using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationsCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeID",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Visitantes_MembroId",
                table: "ListaPessoais");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Visitantes_MembroId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_MembroId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_ListaPessoais_MembroId",
                table: "ListaPessoais");

            migrationBuilder.DropColumn(
                name: "MembroId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Pedidos",
                newName: "UtilizadorId");

            migrationBuilder.RenameColumn(
                name: "FilmeID",
                table: "Avaliacoes",
                newName: "FilmeId");

            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "Avaliacoes",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_FilmeID",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_FilmeId");

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorId",
                table: "ListaPessoais",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UtilizadorId",
                table: "Pedidos",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoais_UtilizadorId",
                table: "ListaPessoais",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_ClienteId_FilmeId",
                table: "Avaliacoes",
                columns: new[] { "ClienteId", "FilmeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Acessos_ClienteId",
                table: "Acessos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acessos_Visitantes_ClienteId",
                table: "Acessos",
                column: "ClienteId",
                principalTable: "Visitantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Visitantes_ClienteId",
                table: "Avaliacoes",
                column: "ClienteId",
                principalTable: "Visitantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPessoais_Visitantes_UtilizadorId",
                table: "ListaPessoais",
                column: "UtilizadorId",
                principalTable: "Visitantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Visitantes_UtilizadorId",
                table: "Pedidos",
                column: "UtilizadorId",
                principalTable: "Visitantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acessos_Visitantes_ClienteId",
                table: "Acessos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Visitantes_ClienteId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Visitantes_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Visitantes_UtilizadorId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UtilizadorId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_ListaPessoais_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_ClienteId_FilmeId",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Acessos_ClienteId",
                table: "Acessos");

            migrationBuilder.DropColumn(
                name: "UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.RenameColumn(
                name: "UtilizadorId",
                table: "Pedidos",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "FilmeId",
                table: "Avaliacoes",
                newName: "FilmeID");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Avaliacoes",
                newName: "ClienteID");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_FilmeId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_FilmeID");

            migrationBuilder.AddColumn<int>(
                name: "MembroId",
                table: "Pedidos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_MembroId",
                table: "Pedidos",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoais_MembroId",
                table: "ListaPessoais",
                column: "MembroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeID",
                table: "Avaliacoes",
                column: "FilmeID",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPessoais_Visitantes_MembroId",
                table: "ListaPessoais",
                column: "MembroId",
                principalTable: "Visitantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Visitantes_MembroId",
                table: "Pedidos",
                column: "MembroId",
                principalTable: "Visitantes",
                principalColumn: "Id");
        }
    }
}
