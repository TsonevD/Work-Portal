using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkPortal.Data.Migrations
{
    public partial class ChangedTheValueTypesInAnnualLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakenDays",
                table: "AnnualLeaves");

            migrationBuilder.AlterColumn<int>(
                name: "RemainingDays",
                table: "AnnualLeaves",
                type: "int",
                maxLength: 28,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysToBeTaken",
                table: "AnnualLeaves",
                type: "int",
                maxLength: 28,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AnnualLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToBeTaken",
                table: "AnnualLeaves");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AnnualLeaves");

            migrationBuilder.AlterColumn<decimal>(
                name: "RemainingDays",
                table: "AnnualLeaves",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 28,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TakenDays",
                table: "AnnualLeaves",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
