using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package05_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Treinamentos");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Treinamentos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Treinamentos",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Treinamentos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Treinamentos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Treinamentos",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
