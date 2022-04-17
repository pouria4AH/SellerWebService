using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class EdirFeatureCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductFeaturesId",
                table: "ProductFeatureCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductFeaturesId",
                table: "ProductFeatureCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
