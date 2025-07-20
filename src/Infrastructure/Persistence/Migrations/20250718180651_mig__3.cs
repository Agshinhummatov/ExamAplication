using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig__3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentNumber",
                table: "Students",
                type: "int",
                precision: 5,
                scale: 0,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,0)",
                oldPrecision: 5);

            migrationBuilder.AlterColumn<int>(
                name: "StudentNumber",
                table: "Exams",
                type: "int",
                precision: 5,
                scale: 0,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,0)",
                oldPrecision: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StudentNumber",
                table: "Students",
                type: "decimal(5,0)",
                precision: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 5,
                oldScale: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "StudentNumber",
                table: "Exams",
                type: "decimal(5,0)",
                precision: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 5,
                oldScale: 0);
        }
    }
}
