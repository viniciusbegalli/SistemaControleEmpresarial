using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treinamentos",
                columns: table => new
                {
                    CodigoTreinamento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    DataInicioTreinamento = table.Column<DateTime>(nullable: false),
                    DataFimTreinamento = table.Column<DateTime>(nullable: false),
                    HoraInicioTreinamento = table.Column<DateTime>(nullable: false),
                    HoraFimTreinamento = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    SituacaoTreinamento = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinamentos", x => x.CodigoTreinamento);
                    table.ForeignKey(
                        name: "FK_Treinamentos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treinamentos_UserId",
                table: "Treinamentos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treinamentos");
        }
    }
}
