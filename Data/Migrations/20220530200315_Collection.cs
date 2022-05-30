using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBuy.Data.Migrations
{
    public partial class Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgeCategory",
                table: "Products",
                newName: "Collection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Collection",
                table: "Products",
                newName: "AgeCategory");
        }
    }
}
