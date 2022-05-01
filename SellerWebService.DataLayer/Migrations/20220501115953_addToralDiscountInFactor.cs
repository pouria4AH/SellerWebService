using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class addToralDiscountInFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainPrice",
                table: "Factors",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<int>(
                name: "taxation",
                table: "Factors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalDiscount",
                table: "Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "Factors");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Factors",
                newName: "MainPrice");

            migrationBuilder.AlterColumn<int>(
                name: "taxation",
                table: "Factors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
