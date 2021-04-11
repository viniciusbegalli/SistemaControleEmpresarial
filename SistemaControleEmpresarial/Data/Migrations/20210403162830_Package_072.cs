using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaControleEmpresarial.Data.Migrations
{
    public partial class Package_072 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VagaCandidatos",
                columns: table => new
                {
                    CodigoVagaCandidato = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoVaga = table.Column<int>(nullable: false),
                    NomeCandidato = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    LinkedIn = table.Column<string>(maxLength: 50, nullable: false),
                    Observacoes = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaCandidatos", x => x.CodigoVagaCandidato);
                    table.ForeignKey(
                        name: "FK_VagaCandidatos_Vagas_CodigoVaga",
                        column: x => x.CodigoVaga,
                        principalTable: "Vagas",
                        principalColumn: "CodigoVaga",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VagaCandidatos_CodigoVaga",
                table: "VagaCandidatos",
                column: "CodigoVaga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VagaCandidatos");
        }
    }
}
