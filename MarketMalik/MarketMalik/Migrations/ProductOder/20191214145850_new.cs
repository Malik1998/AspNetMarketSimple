using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMalik.Migrations.ProductOder
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_odered",
                table: "ProductOders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_odered",
                table: "ProductOders");
        }
    }
}
