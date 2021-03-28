using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontoEletronicos",
                columns: table => new
                {
                    CodigoPontoEletronico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoUsuario = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DataHoraPrimeiraEntrada = table.Column<DateTime>(nullable: true),
                    DataHoraPrimeiraSaida = table.Column<DateTime>(nullable: true),
                    DataHoraSegundaEntrada = table.Column<DateTime>(nullable: true),
                    DataHoraSegundaSaida = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoEletronicos", x => x.CodigoPontoEletronico);
                    table.ForeignKey(
                        name: "FK_PontoEletronicos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontoEletronicos_UserId",
                table: "PontoEletronicos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontoEletronicos");
        }
    }
}
