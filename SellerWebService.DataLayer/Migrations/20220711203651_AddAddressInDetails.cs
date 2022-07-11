using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class AddAddressInDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "StoreDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "StoreDetails");
        }
    }
}
