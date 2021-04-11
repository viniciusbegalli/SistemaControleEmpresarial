using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoUsuarioUltimaAtualizacao",
                table: "Feriados",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CodigoUsuarioUltimaAtualizacao",
                table: "Feriados",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
