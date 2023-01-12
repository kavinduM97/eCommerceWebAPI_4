using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product_categories",
                columns: new[] { "ProductCategoryId", "ProductCategoryDescription", "ProductCategoryName" },
                values: new object[] { 1, "This is and 15w", "Electrical Db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product_categories",
                keyColumn: "ProductCategoryId",
                keyValue: 1);
        }
    }
}
