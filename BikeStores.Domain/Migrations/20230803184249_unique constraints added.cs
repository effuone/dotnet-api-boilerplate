using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeStores.Domain.Migrations
{
    public partial class uniqueconstraintsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_products_product_name",
                schema: "production",
                table: "products",
                column: "product_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_email",
                schema: "sales",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_category_name",
                schema: "production",
                table: "categories",
                column: "category_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_brands_brand_name",
                schema: "production",
                table: "brands",
                column: "brand_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_product_name",
                schema: "production",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_customers_email",
                schema: "sales",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_categories_category_name",
                schema: "production",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_brands_brand_name",
                schema: "production",
                table: "brands");
        }
    }
}
