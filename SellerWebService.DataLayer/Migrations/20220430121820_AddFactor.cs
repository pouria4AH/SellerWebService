using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class AddFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFirstPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalPaid = table.Column<bool>(type: "bit", nullable: false),
                    FirstTracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FinalTracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    MainPrice = table.Column<long>(type: "bigint", nullable: false),
                    FinalPrice = table.Column<long>(type: "bigint", nullable: false),
                    DiscountRate = table.Column<int>(type: "int", nullable: false),
                    Prepayment = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    FactorStatus = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorFeatureSelecteds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFeatureId = table.Column<long>(type: "bigint", nullable: false),
                    FactorId = table.Column<long>(type: "bigint", nullable: false),
                    GroupForProductFeatureId = table.Column<long>(type: "bigint", nullable: false),
                    PriceOfFeature = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FactorFeatureSelecteds_ProductFeatures_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.CreateIndex(
                name: "IX_Factors_ProductId",
                table: "Factors",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_UserId",
                table: "Factors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorFeatureSelecteds");

            migrationBuilder.DropTable(
                name: "Factors");
        }
    }
}
