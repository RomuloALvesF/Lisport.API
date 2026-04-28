using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lisport.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniformeToAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TemUniforme",
                table: "Alunos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecebeuUniforme",
                table: "Alunos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TemUniforme", table: "Alunos");
            migrationBuilder.DropColumn(name: "RecebeuUniforme", table: "Alunos");
        }
    }
}
