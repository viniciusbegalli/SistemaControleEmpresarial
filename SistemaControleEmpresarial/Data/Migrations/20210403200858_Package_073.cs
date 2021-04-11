using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_073 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraUltimaAlteracao",
                table: "Vagas",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "VagaCandidatos",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    CodigoChamado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    SituacaoChamado = table.Column<string>(nullable: true),
                    Fila = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.CodigoChamado);
                    table.ForeignKey(
                        name: "FK_Chamados_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChamadoNotas",
                columns: table => new
                {
                    CodigoChamadoNota = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoChamado = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DataNota = table.Column<DateTime>(nullable: false),
                    DescricaoNota = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadoNotas", x => x.CodigoChamadoNota);
                    table.ForeignKey(
                        name: "FK_ChamadoNotas_Chamados_CodigoChamado",
                        column: x => x.CodigoChamado,
                        principalTable: "Chamados",
                        principalColumn: "CodigoChamado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChamadoNotas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoNotas_CodigoChamado",
                table: "ChamadoNotas",
                column: "CodigoChamado");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoNotas_UserId",
                table: "ChamadoNotas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_UserId",
                table: "Chamados",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChamadoNotas");

            migrationBuilder.DropTable(
                name: "Chamados");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraUltimaAlteracao",
                table: "Vagas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "VagaCandidatos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
