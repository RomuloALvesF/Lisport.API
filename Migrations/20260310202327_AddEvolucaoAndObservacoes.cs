using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lisport.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEvolucaoAndObservacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evolucoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Periodo = table.Column<string>(type: "TEXT", nullable: false),
                    EvolucaoFisica = table.Column<int>(type: "INTEGER", nullable: false),
                    EvolucaoTecnica = table.Column<int>(type: "INTEGER", nullable: false),
                    Comportamento = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolucoes_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesGerais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TurmaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Texto = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesGerais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesGerais_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ObservacoesGerais_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ObservacoesGerais_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evolucoes_AlunoId",
                table: "Evolucoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesGerais_AlunoId",
                table: "ObservacoesGerais",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesGerais_CreatedByUserId",
                table: "ObservacoesGerais",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesGerais_TurmaId",
                table: "ObservacoesGerais",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evolucoes");

            migrationBuilder.DropTable(
                name: "ObservacoesGerais");
        }
    }
}
