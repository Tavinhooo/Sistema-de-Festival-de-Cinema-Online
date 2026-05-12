using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFestivalFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Filmes_FilmeId",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido");

            migrationBuilder.DropTable(
                name: "ItensCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensPedido",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Filmes");

            migrationBuilder.RenameTable(
                name: "ItensPedido",
                newName: "Itens");

            migrationBuilder.RenameIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "Itens",
                newName: "IX_Itens_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_ItensPedido_FilmeId",
                table: "Itens",
                newName: "IX_Itens_FilmeId");

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "Itens",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Itens",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "Itens",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Itens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Itens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itens",
                table: "Itens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UtilizadorId = table.Column<int>(type: "integer", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValorTotal = table.Column<double>(type: "double precision", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "text", nullable: false),
                    ReferenciaPagamento = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FestivalFilme",
                columns: table => new
                {
                    FestivaisId = table.Column<int>(type: "integer", nullable: false),
                    FilmesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalFilme", x => new { x.FestivaisId, x.FilmesId });
                    table.ForeignKey(
                        name: "FK_FestivalFilme_Festivais_FestivaisId",
                        column: x => x.FestivaisId,
                        principalTable: "Festivais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FestivalFilme_Filmes_FilmesId",
                        column: x => x.FilmesId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_CarrinhoId",
                table: "Itens",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_CompraId",
                table: "Itens",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_FestivalFilme_FilmesId",
                table: "FestivalFilme",
                column: "FilmesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Carrinhos_CarrinhoId",
                table: "Itens",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Compras_CompraId",
                table: "Itens",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Filmes_FilmeId",
                table: "Itens",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Pedidos_PedidoId",
                table: "Itens",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Carrinhos_CarrinhoId",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Compras_CompraId",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Filmes_FilmeId",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Pedidos_PedidoId",
                table: "Itens");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "FestivalFilme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itens",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Itens_CarrinhoId",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Itens_CompraId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Itens");

            migrationBuilder.RenameTable(
                name: "Itens",
                newName: "ItensPedido");

            migrationBuilder.RenameIndex(
                name: "IX_Itens_PedidoId",
                table: "ItensPedido",
                newName: "IX_ItensPedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Itens_FilmeId",
                table: "ItensPedido",
                newName: "IX_ItensPedido_FilmeId");

            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Filmes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "ItensPedido",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensPedido",
                table: "ItensPedido",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItensCarrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmeId = table.Column<int>(type: "integer", nullable: false),
                    CarrinhoId = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<double>(type: "double precision", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    TipoAcesso = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCarrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_CarrinhoId",
                table: "ItensCarrinho",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_FilmeId",
                table: "ItensCarrinho",
                column: "FilmeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes",
                column: "FestivalId",
                principalTable: "Festivais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Filmes_FilmeId",
                table: "ItensPedido",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedidos_PedidoId",
                table: "ItensPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
