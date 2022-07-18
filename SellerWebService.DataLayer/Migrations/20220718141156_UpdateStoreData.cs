using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class UpdateStoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirePanel",
                table: "StoreDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LinkSlug",
                table: "StoreDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PanelModel",
                table: "StoreDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserNameId",
                table: "StoreDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirePanel",
                table: "StoreDatas");

            migrationBuilder.DropColumn(
                name: "LinkSlug",
                table: "StoreDatas");

            migrationBuilder.DropColumn(
                name: "PanelModel",
                table: "StoreDatas");

            migrationBuilder.DropColumn(
                name: "UserNameId",
                table: "StoreDatas");
        }
    }
}
