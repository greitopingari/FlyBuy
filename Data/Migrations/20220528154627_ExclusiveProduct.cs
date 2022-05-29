using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBuy.Data.Migrations
{
    public partial class ExclusiveProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Exclusive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exclusive",
                table: "Products");
        }
    }
}
