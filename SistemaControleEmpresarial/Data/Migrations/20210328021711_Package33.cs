using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoEletronicos_AspNetUsers_UserId",
                table: "PontoEletronicos");

            migrationBuilder.DropColumn(
                name: "CodigoUsuario",
                table: "PontoEletronicos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PontoEletronicos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PontoEletronicos_AspNetUsers_UserId",
                table: "PontoEletronicos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoEletronicos_AspNetUsers_UserId",
                table: "PontoEletronicos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PontoEletronicos",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CodigoUsuario",
                table: "PontoEletronicos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PontoEletronicos_AspNetUsers_UserId",
                table: "PontoEletronicos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
