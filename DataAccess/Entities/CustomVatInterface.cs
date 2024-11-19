namespace DataAccess.Entities
{
    public class CustomVatInterface
    {
        public int Id { get; set; } // Primary Key
        public int DocumentId { get; set; }
        public decimal? TaxBase { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? VatRate { get; set; }
        public bool? IsTaxExempt { get; set; }
    }
}
