﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class AddGropeForProductFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_Products_ProductId",
                table: "ProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_ProductFeatureCategoryId",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "ProductFeatureCategoryId",
                table: "ProductFeatures");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductFeatures",
                newName: "GroupForProductFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatures_ProductId",
                table: "ProductFeatures",
                newName: "IX_ProductFeatures_GroupForProductFeatureId");

            migrationBuilder.CreateTable(
                name: "GroupForProductFeature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductFeatureCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupForProductFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupForProductFeature_ProductFeatureCategories_ProductFeatureCategoryId",
                        column: x => x.ProductFeatureCategoryId,
                        principalTable: "ProductFeatureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupForProductFeature_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeature_ProductFeatureCategoryId",
                table: "GroupForProductFeature",
                column: "ProductFeatureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeature_ProductId",
                table: "GroupForProductFeature",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeature_GroupForProductFeatureId",
                table: "ProductFeatures",
                column: "GroupForProductFeatureId",
                principalTable: "GroupForProductFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_GroupForProductFeature_GroupForProductFeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "GroupForProductFeature");

            migrationBuilder.RenameColumn(
                name: "GroupForProductFeatureId",
                table: "ProductFeatures",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures",
                newName: "IX_ProductFeatures_ProductId");

            migrationBuilder.AddColumn<long>(
                name: "ProductFeatureCategoryId",
                table: "ProductFeatures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_ProductFeatureCategoryId",
                table: "ProductFeatures",
                column: "ProductFeatureCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                table: "ProductFeatures",
                column: "ProductFeatureCategoryId",
                principalTable: "ProductFeatureCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_Products_ProductId",
                table: "ProductFeatures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
