using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class EditFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors");

            migrationBuilder.DropTable(
                name: "FactorFeatureSelecteds");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Factors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Factors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Factors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "FactorFeatureSelecteds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorId = table.Column<long>(type: "bigint", nullable: false),
                    GroupForProductFeatureId = table.Column<long>(type: "bigint", nullable: false),
                    ProductFeatureId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceOfFeature = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorFeatureSelecteds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorFeatureSelecteds_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorFeatureSelecteds_GroupForProductFeatures_GroupForProductFeatureId",
                        column: x => x.GroupForProductFeatureId,
                        principalTable: "GroupForProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorFeatureSelecteds_ProductFeatures_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactorFeatureSelecteds_FactorId",
                table: "FactorFeatureSelecteds",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorFeatureSelecteds_GroupForProductFeatureId",
                table: "FactorFeatureSelecteds",
                column: "GroupForProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorFeatureSelecteds_ProductFeatureId",
                table: "FactorFeatureSelecteds",
                column: "ProductFeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
