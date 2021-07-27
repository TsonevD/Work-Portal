using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkPortal.Data.Migrations
{
    public partial class UpdatedAnnualLeaveWithReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "AnnualLeaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "AnnualLeaves");
        }
    }
}
