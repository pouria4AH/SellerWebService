using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class changeForV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSelectedCategories");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CountArray",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExrernalLink",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InternalLink",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StateForCount",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "StoreDataId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StoreDataId",
                table: "ProductFeatures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StoreDataId",
                table: "ProductFeatureCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreDataId",
                table: "Products",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_StoreDataId",
                table: "ProductFeatures",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureCategories_StoreDataId",
                table: "ProductFeatureCategories",
                column: "StoreDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureCategories_StoreDatas_StoreDataId",
                table: "ProductFeatureCategories",
                column: "StoreDataId",
                principalTable: "StoreDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_StoreDatas_StoreDataId",
                table: "ProductFeatures",
                column: "StoreDataId",
                principalTable: "StoreDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StoreDatas_StoreDataId",
                table: "Products",
                column: "StoreDataId",
                principalTable: "StoreDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureCategories_StoreDatas_StoreDataId",
                table: "ProductFeatureCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_StoreDatas_StoreDataId",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoreDatas_StoreDataId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreDataId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_StoreDataId",
                table: "ProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureCategories_StoreDataId",
                table: "ProductFeatureCategories");

            migrationBuilder.DropColumn(
                name: "StoreDataId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreDataId",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "StoreDataId",
                table: "ProductFeatureCategories");

            migrationBuilder.AddColumn<string>(
                name: "CountArray",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExrernalLink",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternalLink",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateForCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExrernalLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PictureAlt = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PictureTitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SeoTitle = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSelectedCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategories_ProductCategoryId",
                table: "ProductSelectedCategories",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategories_ProductId",
                table: "ProductSelectedCategories",
                column: "ProductId");
        }
    }
}
