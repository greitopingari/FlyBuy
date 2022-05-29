using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBuy.Data.Migrations
{
    public partial class NewFieldContactUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ContactUs");
        }
    }
}
