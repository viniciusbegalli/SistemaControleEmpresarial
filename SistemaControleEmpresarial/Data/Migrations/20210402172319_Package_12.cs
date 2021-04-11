using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoEquipe",
                table: "SolicitacaoJornadas");

            migrationBuilder.DropColumn(
                name: "NomeUsuarioCriacao",
                table: "SolicitacaoJornadas");

            migrationBuilder.AlterColumn<int>(
                name: "CodigoUsuarioJornada",
                table: "SolicitacaoJornadas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoUsuarioJornada",
                table: "SolicitacaoJornadas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CodigoEquipe",
                table: "SolicitacaoJornadas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeUsuarioCriacao",
                table: "SolicitacaoJornadas",
                nullable: true);
        }
    }
}
