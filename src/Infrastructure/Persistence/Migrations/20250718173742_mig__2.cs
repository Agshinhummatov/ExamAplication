using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig__2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Exams",
                type: "int",
                precision: 1,
                scale: 0,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,0)",
                oldPrecision: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Grade",
                table: "Exams",
                type: "decimal(1,0)",
                precision: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 1,
                oldScale: 0);
        }
    }
}
