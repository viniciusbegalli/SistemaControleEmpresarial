using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package04_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoExterno",
                table: "TreinamentoInstrutores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeInstrutor",
                table: "TreinamentoInstrutores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodigoExterno",
                table: "ChamadoNotas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeUsuarioNota",
                table: "ChamadoNotas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoExterno",
                table: "TreinamentoInstrutores");

            migrationBuilder.DropColumn(
                name: "NomeInstrutor",
                table: "TreinamentoInstrutores");

            migrationBuilder.DropColumn(
                name: "CodigoExterno",
                table: "ChamadoNotas");

            migrationBuilder.DropColumn(
                name: "NomeUsuarioNota",
                table: "ChamadoNotas");
        }
    }
}
