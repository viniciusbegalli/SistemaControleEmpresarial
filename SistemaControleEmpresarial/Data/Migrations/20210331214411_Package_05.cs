using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAlteracoes",
                columns: table => new
                {
                    CodigoLogAlteracao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    TipoOperacaoLog = table.Column<string>(nullable: true),
                    Entidade = table.Column<string>(nullable: true),
                    CodigoEntidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAlteracoes", x => x.CodigoLogAlteracao);
                    table.ForeignKey(
                        name: "FK_LogAlteracoes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogAlteracaoItems",
                columns: table => new
                {
                    CodigoLogAlteracaoItem = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoLogAlteracao = table.Column<int>(nullable: false),
                    Objeto = table.Column<string>(nullable: true),
                    ValorAntigo = table.Column<string>(nullable: true),
                    ValorNovo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAlteracaoItems", x => x.CodigoLogAlteracaoItem);
                    table.ForeignKey(
                        name: "FK_LogAlteracaoItems_LogAlteracoes_CodigoLogAlteracao",
                        column: x => x.CodigoLogAlteracao,
                        principalTable: "LogAlteracoes",
                        principalColumn: "CodigoLogAlteracao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogAlteracaoItems_CodigoLogAlteracao",
                table: "LogAlteracaoItems",
                column: "CodigoLogAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_LogAlteracoes_UserId",
                table: "LogAlteracoes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAlteracaoItems");

            migrationBuilder.DropTable(
                name: "LogAlteracoes");
        }
    }
}
