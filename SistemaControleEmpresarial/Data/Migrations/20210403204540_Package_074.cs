using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_074 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Fila",
                table: "Chamados",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prioridade",
                table: "Chamados",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Chamados");

            migrationBuilder.AlterColumn<string>(
                name: "Fila",
                table: "Chamados",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
