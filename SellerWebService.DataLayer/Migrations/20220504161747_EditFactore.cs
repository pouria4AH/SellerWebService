using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class EditFactore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Factors",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Factors_UserId",
                table: "Factors",
                newName: "IX_Factors_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Customers_CustomerId",
                table: "Factors",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Customers_CustomerId",
                table: "Factors");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Factors",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Factors_CustomerId",
                table: "Factors",
                newName: "IX_Factors_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
