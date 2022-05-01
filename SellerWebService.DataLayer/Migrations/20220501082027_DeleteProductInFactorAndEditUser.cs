using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class DeleteProductInFactorAndEditUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors");

            migrationBuilder.DropIndex(
                name: "IX_Factors_ProductId",
                table: "Factors");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Factors");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Factors",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factors_ProductId",
                table: "Factors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Products_ProductId",
                table: "Factors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
