using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DescricaoFeriado",
                table: "Feriados",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DescricaoFeriado",
                table: "Feriados",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
