using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameVisitantesToUtilizadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acessos_Visitantes_ClienteId",
                table: "Acessos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Visitantes_ClienteId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Visitantes_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsAlteracaoAcessos_Visitantes_AdministradorId",
                table: "LogsAlteracaoAcessos");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsAlteracaoAcessos_Visitantes_UtilizadorId",
                table: "LogsAlteracaoAcessos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Visitantes_UtilizadorId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitantes",
                table: "Visitantes");

            migrationBuilder.RenameTable(
                name: "Visitantes",
                newName: "Utilizadores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acessos_Utilizadores_ClienteId",
                table: "Acessos",
                column: "ClienteId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Utilizadores_ClienteId",
                table: "Avaliacoes",
                column: "ClienteId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPessoais_Utilizadores_UtilizadorId",
                table: "ListaPessoais",
                column: "UtilizadorId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogsAlteracaoAcessos_Utilizadores_AdministradorId",
                table: "LogsAlteracaoAcessos",
                column: "AdministradorId",
                principalTable: "Utilizadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogsAlteracaoAcessos_Utilizadores_UtilizadorId",
                table: "LogsAlteracaoAcessos",
                column: "UtilizadorId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Utilizadores_UtilizadorId",
                table: "Pedidos",
                column: "UtilizadorId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acessos_Utilizadores_ClienteId",
                table: "Acessos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Utilizadores_ClienteId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaPessoais_Utilizadores_UtilizadorId",
                table: "ListaPessoais");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsAlteracaoAcessos_Utilizadores_AdministradorId",
                table: "LogsAlteracaoAcessos");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsAlteracaoAcessos_Utilizadores_UtilizadorId",
                table: "LogsAlteracaoAcessos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Utilizadores_UtilizadorId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores");

            migrationBuilder.RenameTable(
                name: "Utilizadores",
                newName: "Visitantes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitantes",
                table: "Visitantes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acessos_Visitantes_ClienteId",
                table: "Acessos",
                column: "ClienteId",
                principalTable: "Visitantes",
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
                name: "FK_LogsAlteracaoAcessos_Visitantes_AdministradorId",
                table: "LogsAlteracaoAcessos",
                column: "AdministradorId",
                principalTable: "Visitantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogsAlteracaoAcessos_Visitantes_UtilizadorId",
                table: "LogsAlteracaoAcessos",
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
    }
}
