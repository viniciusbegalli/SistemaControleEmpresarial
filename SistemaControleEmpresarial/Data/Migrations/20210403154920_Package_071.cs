using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_071 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreinamentoInstrutores",
                columns: table => new
                {
                    CodigoTreinamentoInstrutor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoTreinamento = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinamentoInstrutores", x => x.CodigoTreinamentoInstrutor);
                    table.ForeignKey(
                        name: "FK_TreinamentoInstrutores_Treinamentos_CodigoTreinamento",
                        column: x => x.CodigoTreinamento,
                        principalTable: "Treinamentos",
                        principalColumn: "CodigoTreinamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinamentoInstrutores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    CodigoVaga = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    SituacaoVaga = table.Column<string>(nullable: true),
                    CodigoUsuarioUltimaAtualizacao = table.Column<string>(nullable: true),
                    NomeUsuarioUltimaAtualizacao = table.Column<string>(nullable: true),
                    DataHoraUltimaAlteracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.CodigoVaga);
                    table.ForeignKey(
                        name: "FK_Vagas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreinamentoInstrutores_CodigoTreinamento",
                table: "TreinamentoInstrutores",
                column: "CodigoTreinamento");

            migrationBuilder.CreateIndex(
                name: "IX_TreinamentoInstrutores_UserId",
                table: "TreinamentoInstrutores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_UserId",
                table: "Vagas",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreinamentoInstrutores");

            migrationBuilder.DropTable(
                name: "Vagas");
        }
    }
}
