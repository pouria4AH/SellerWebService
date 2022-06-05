using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class ChangeProductAndGroupFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prepayment",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "StoreDataId",
                table: "GroupForProductFeatures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeatures_StoreDataId",
                table: "GroupForProductFeatures",
                column: "StoreDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupForProductFeatures_StoreDatas_StoreDataId",
                table: "GroupForProductFeatures",
                column: "StoreDataId",
                principalTable: "StoreDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupForProductFeatures_StoreDatas_StoreDataId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_GroupForProductFeatures_StoreDataId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropColumn(
                name: "StoreDataId",
                table: "GroupForProductFeatures");

            migrationBuilder.AddColumn<double>(
                name: "Prepayment",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
