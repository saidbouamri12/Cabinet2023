using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet.EF.Migrations
{
    public partial class appoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentType",
                table: "Appointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentType",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
