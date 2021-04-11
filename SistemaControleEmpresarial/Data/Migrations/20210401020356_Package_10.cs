using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitacaoJornadas",
                columns: table => new
                {
                    CodigoSolicitacaoJornada = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoFeriado = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    NomeUsuarioCriacao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    CodigoUsuarioJornada = table.Column<string>(nullable: true),
                    NomeUsuarioJornada = table.Column<string>(nullable: true),
                    CodigoEquipe = table.Column<int>(nullable: false),
                    Justificativa = table.Column<string>(nullable: true),
                    SituacaoSolicitacao = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoJornadas", x => x.CodigoSolicitacaoJornada);
                    table.ForeignKey(
                        name: "FK_SolicitacaoJornadas_Feriados_CodigoFeriado",
                        column: x => x.CodigoFeriado,
                        principalTable: "Feriados",
                        principalColumn: "CodigoFeriado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoJornadas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoJornadas_CodigoFeriado",
                table: "SolicitacaoJornadas",
                column: "CodigoFeriado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoJornadas_UserId",
                table: "SolicitacaoJornadas",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoJornadas");
        }
    }
}
