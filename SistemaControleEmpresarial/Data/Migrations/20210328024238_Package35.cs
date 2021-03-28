using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AjustePontoEletronicos",
                columns: table => new
                {
                    CodigoAjuste = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(nullable: true),
                    DataAjuste = table.Column<DateTime>(nullable: false),
                    HoraPrimeiraEntrada = table.Column<DateTime>(nullable: false),
                    HoraPrimeiraSaida = table.Column<DateTime>(nullable: false),
                    HoraSegundaEntrada = table.Column<DateTime>(nullable: false),
                    HoraSegundaSaida = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    SituacaoAjuste = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustePontoEletronicos", x => x.CodigoAjuste);
                    table.ForeignKey(
                        name: "FK_AjustePontoEletronicos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AjustePontoEletronicos_UsuarioId",
                table: "AjustePontoEletronicos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustePontoEletronicos");
        }
    }
}
