using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet.EF.Migrations
{
    public partial class First1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statuspack",
                table: "Packs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "statuspack",
                table: "Packs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
