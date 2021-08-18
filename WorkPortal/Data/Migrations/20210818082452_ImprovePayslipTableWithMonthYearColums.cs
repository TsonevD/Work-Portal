using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkPortal.Data.Migrations
{
    public partial class ImprovePayslipTableWithMonthYearColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuedOn",
                table: "Payslips");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Payslips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Payslips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DaysToBeTaken",
                table: "AnnualLeaves",
                type: "int",
                maxLength: 28,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 28,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Payslips");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedOn",
                table: "Payslips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "DaysToBeTaken",
                table: "AnnualLeaves",
                type: "int",
                maxLength: 28,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 28);
        }
    }
}
