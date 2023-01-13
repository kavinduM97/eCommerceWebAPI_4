using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "categoryId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "categoryId", "description", "name" },
                values: new object[] { 1, "This is and 15w", "Electrical Db" });
        }
    }
}
