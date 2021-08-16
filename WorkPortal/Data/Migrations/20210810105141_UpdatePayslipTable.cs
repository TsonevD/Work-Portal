using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace WorkPortal.Data.Migrations
{
    [ExcludeFromCodeCoverage]

    public partial class UpdatePayslipTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayslip");

            migrationBuilder.RenameColumn(
                name: "WorkingHourPerMonth",
                table: "Payslips",
                newName: "WorkingHoursPerMonth");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Payslips",
                newName: "Taxes");

            migrationBuilder.RenameColumn(
                name: "RatePerHour",
                table: "Payslips",
                newName: "BeforeTaxSalary");

            migrationBuilder.AddColumn<decimal>(
                name: "AfterTaxSalary",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Payslips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payslips_Employees_EmployeeId",
                table: "Payslips",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payslips_Employees_EmployeeId",
                table: "Payslips");

            migrationBuilder.DropIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "AfterTaxSalary",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Payslips");

            migrationBuilder.RenameColumn(
                name: "WorkingHoursPerMonth",
                table: "Payslips",
                newName: "WorkingHourPerMonth");

            migrationBuilder.RenameColumn(
                name: "Taxes",
                table: "Payslips",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "BeforeTaxSalary",
                table: "Payslips",
                newName: "RatePerHour");

            migrationBuilder.CreateTable(
                name: "EmployeePayslip",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    PayslipsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayslip", x => new { x.EmployeesId, x.PayslipsId });
                    table.ForeignKey(
                        name: "FK_EmployeePayslip_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayslip_Payslips_PayslipsId",
                        column: x => x.PayslipsId,
                        principalTable: "Payslips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayslip_PayslipsId",
                table: "EmployeePayslip",
                column: "PayslipsId");
        }
    }
}
