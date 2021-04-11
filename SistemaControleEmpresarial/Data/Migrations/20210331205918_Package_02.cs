using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "AjustePontoEletronicos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "AjustePontoEletronicos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "Feriados",
                columns: table => new
                {
                    CodigoFeriado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescricaoFeriado = table.Column<string>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    CodigoUsuarioUltimaAtualizacao = table.Column<int>(nullable: false),
                    DataHoraUltimaAlteracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriados", x => x.CodigoFeriado);
                    table.ForeignKey(
                        name: "FK_Feriados_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_UserId",
                table: "Feriados",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feriados");

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "AjustePontoEletronicos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "AjustePontoEletronicos",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    CodigoFuncionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<string>(nullable: false),
                    CodigoUsuario = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    NomeUsuario = table.Column<string>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.CodigoFuncionario);
                    table.ForeignKey(
                        name: "FK_Funcionarios_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_UserId",
                table: "Funcionarios",
                column: "UserId");
        }
    }
}
