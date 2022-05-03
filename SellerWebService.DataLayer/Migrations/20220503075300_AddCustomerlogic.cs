using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class AddCustomerlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupForProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupForProductFeatures_Products_ProductId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreCode",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StoreDataId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreCode",
                table: "Factors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StoreDatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    UniqueCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelegramNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SigntureImage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StampImage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreDetails_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreDataId",
                table: "Users",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StoreDataId",
                table: "Customers",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDetails_StoreDataId",
                table: "StoreDetails",
                column: "StoreDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails",
                column: "FactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupForProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "GroupForProductFeatures",
                column: "ProductFeatureCategoryId",
                principalTable: "ProductFeatureCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupForProductFeatures_Products_ProductId",
                table: "GroupForProductFeatures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures",
                column: "GroupForProductFeatureId",
                principalTable: "GroupForProductFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_StoreDatas_StoreDataId",
                table: "Users",
                column: "StoreDataId",
                principalTable: "StoreDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupForProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupForProductFeatures_Products_ProductId",
                table: "GroupForProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_StoreDatas_StoreDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "StoreDetails");

            migrationBuilder.DropTable(
                name: "StoreDatas");

            migrationBuilder.DropIndex(
                name: "IX_Users_StoreDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StoreCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StoreDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StoreCode",
                table: "Factors");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails",
                column: "FactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupForProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "GroupForProductFeatures",
                column: "ProductFeatureCategoryId",
                principalTable: "ProductFeatureCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupForProductFeatures_Products_ProductId",
                table: "GroupForProductFeatures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures",
                column: "GroupForProductFeatureId",
                principalTable: "GroupForProductFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
