using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentErpId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IssuedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VatRateBasic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxBaseBasic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatAmountBasic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatRateReduced = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxBaseReduced = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatAmountReduced = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatRateFree = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxBaseFree = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatAmountFree = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    accountingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    organizationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organizationVAT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organizationVatId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organizationTaxId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organizationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    streetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    postalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    invoiceNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountingSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    AccountingItem_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    AccountingItem_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountingItem_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountingItem_ResourceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericCode_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    NumericCode_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericCode_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericCode_PaymentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CashRegister_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    CashRegister_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CashRegister_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Project_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    Project_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Project_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    Order_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Activity_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    Activity_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Activity_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpenditureCenter_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    ExpenditureCenter_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpenditureCenter_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GeneralDocumentAgenda_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    GeneralDocumentAgenda_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GeneralDocumentAgenda_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountingSoftwareAgenda_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    AccountingSoftwareAgenda_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountingSoftwareAgenda_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PredefinedNote_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    PredefinedNote_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassificationVat_AccountingSoftwareId = table.Column<int>(type: "int", nullable: false),
                    ClassificationVat_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassificationVat_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassificationVat_ValidFrom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassificationVat_ValidTo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassificationVat_isReverseCharge = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PropertyRegistrationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    TaxBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isTaxExempt = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccountingSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
