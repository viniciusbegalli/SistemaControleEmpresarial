using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package04_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreinamentoParticipantes",
                columns: table => new
                {
                    CodigoTreinamentoParticipante = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoTreinamento = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinamentoParticipantes", x => x.CodigoTreinamentoParticipante);
                    table.ForeignKey(
                        name: "FK_TreinamentoParticipantes_Treinamentos_CodigoTreinamento",
                        column: x => x.CodigoTreinamento,
                        principalTable: "Treinamentos",
                        principalColumn: "CodigoTreinamento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreinamentoParticipantes_CodigoTreinamento",
                table: "TreinamentoParticipantes",
                column: "CodigoTreinamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreinamentoParticipantes");
        }
    }
}
