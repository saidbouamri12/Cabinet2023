using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet.EF.Migrations
{
    public partial class First2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "change",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "change",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
