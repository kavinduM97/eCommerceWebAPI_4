using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatecart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categoryName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoryName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Carts");
        }
    }
}
