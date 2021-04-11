using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "AjustePontoEletronicos");

            migrationBuilder.AddColumn<string>(
                name: "Justificativa",
                table: "AjustePontoEletronicos",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Justificativa",
                table: "AjustePontoEletronicos");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "AjustePontoEletronicos",
                nullable: false,
                defaultValue: "");
        }
    }
}
