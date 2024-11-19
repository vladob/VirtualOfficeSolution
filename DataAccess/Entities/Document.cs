using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Document
    {
        private string? accountingCode1;

        public int Id { get; set; } // Primary Key
        public string? ScanedForCompany { get; set; }
        public string? DocumentErpId { get; set; }
        public string? Type { get; set; }
        public string? ReceiptUID { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? Currency { get; set; }
        public decimal? VatRateBasic { get; set; }
        public decimal? TaxBaseBasic { get; set; }
        public decimal? VatAmountBasic { get; set; }
        public decimal? VatRateReduced { get; set; }
        public decimal? TaxBaseReduced { get; set; }
        public decimal? VatAmountReduced { get; set; }
        public decimal? VatRateFree { get; set; }
        public decimal? TaxBaseFree { get; set; }
        public decimal? VatAmountFree { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? AccountingCode { get; set; }
        public string? OrganizationId { get; set; }
        public string? OrganizationVAT { get; set; }
        public string? OrganizationVatId { get; set; }
        public string? OrganizationTaxId { get; set; }
        public string? OrganizationName { get; set; }
        public string? StreetName { get; set; }
        public string? Municipality { get; set; }
        public string? PostalCode { get; set; }
        public string? Email { get; set; }
        public string? PaymentType { get; set; }
        public string? InvoiceNumber { get; set; }
        public ICollection<ExportItem>? Items { get; set; }
        public Address? Address { get; set; }
        public ICollection<CustomVatInterface>? VatSummary { get; set; }
        public AccountingSettingsElement? AccountingSettings { get; set; }
    }
}
