using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class AddStorePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorePayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    TracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorePayment_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorePayment_StoreDataId",
                table: "StorePayment",
                column: "StoreDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorePayment");
        }
    }
}
