using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class EditProductCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureAddress",
                table: "Products",
                newName: "PictureName");

            migrationBuilder.RenameColumn(
                name: "PictureAddress",
                table: "ProductCategories",
                newName: "PictureName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureName",
                table: "Products",
                newName: "PictureAddress");

            migrationBuilder.RenameColumn(
                name: "PictureName",
                table: "ProductCategories",
                newName: "PictureAddress");
        }
    }
}
