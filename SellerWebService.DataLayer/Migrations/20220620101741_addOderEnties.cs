using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerWebService.DataLayer.Migrations
{
    public partial class addOderEnties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreDatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankDatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShabaNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDatas_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatureCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatureCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatureCategories_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefaultPrice = table.Column<long>(type: "bigint", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelegramNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SigntureImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StampImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreDetails_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorePayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", maxLength: 200, nullable: false),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: true),
                    IsForService = table.Column<bool>(type: "bit", nullable: false),
                    FactorCode = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsReadByOwner = table.Column<bool>(type: "bit", nullable: false),
                    IsReadByaSender = table.Column<bool>(type: "bit", nullable: false),
                    TicketSection = table.Column<int>(type: "int", nullable: false),
                    TicketState = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmailActiveCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEmailActive = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsMobileActive = table.Column<bool>(type: "bit", nullable: false),
                    MobileActiveCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UniqueCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFirstPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalPaid = table.Column<bool>(type: "bit", nullable: false),
                    FirstTracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FinalTracingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    TotalDiscount = table.Column<long>(type: "bigint", nullable: false),
                    FinalPrice = table.Column<long>(type: "bigint", nullable: false),
                    Prepayment = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<int>(type: "int", nullable: false),
                    taxation = table.Column<int>(type: "int", nullable: false),
                    StoreCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorStatus = table.Column<int>(type: "int", nullable: false),
                    FirstFactorPaymentState = table.Column<int>(type: "int", nullable: false),
                    FinalFactorPaymentState = table.Column<int>(type: "int", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factors_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupForProductFeatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductFeatureCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupForProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupForProductFeatures_ProductFeatureCategories_ProductFeatureCategoryId",
                        column: x => x.ProductFeatureCategoryId,
                        principalTable: "ProductFeatureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupForProductFeatures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupForProductFeatures_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGalleries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    DisplayPriority = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PictureAlt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGalleries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketsMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsMessages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactorDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Packaging = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorDetails_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    GroupForProductFeatureId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    ExtraPrice = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_GroupForProductFeatures_GroupForProductFeatureId",
                        column: x => x.GroupForProductFeatureId,
                        principalTable: "GroupForProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_StoreDatas_StoreDataId",
                        column: x => x.StoreDataId,
                        principalTable: "StoreDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDatas_StoreDataId",
                table: "BankDatas",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StoreDataId",
                table: "Customers",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetails_FactorId",
                table: "FactorDetails",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_CustomerId",
                table: "Factors",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeatures_ProductFeatureCategoryId",
                table: "GroupForProductFeatures",
                column: "ProductFeatureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeatures_ProductId",
                table: "GroupForProductFeatures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupForProductFeatures_StoreDataId",
                table: "GroupForProductFeatures",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureCategories_StoreDataId",
                table: "ProductFeatureCategories",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_GroupForProductFeatureId",
                table: "ProductFeatures",
                column: "GroupForProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_StoreDataId",
                table: "ProductFeatures",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreDataId",
                table: "Products",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDetails_StoreDataId",
                table: "StoreDetails",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePayment_StoreDataId",
                table: "StorePayment",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StoreDataId",
                table: "Tickets",
                column: "StoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsMessages_TicketId",
                table: "TicketsMessages",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreDataId",
                table: "Users",
                column: "StoreDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankDatas");

            migrationBuilder.DropTable(
                name: "FactorDetails");

            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "ProductGalleries");

            migrationBuilder.DropTable(
                name: "StoreDetails");

            migrationBuilder.DropTable(
                name: "StorePayment");

            migrationBuilder.DropTable(
                name: "TicketsMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Factors");

            migrationBuilder.DropTable(
                name: "GroupForProductFeatures");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ProductFeatureCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StoreDatas");
        }
    }
}
