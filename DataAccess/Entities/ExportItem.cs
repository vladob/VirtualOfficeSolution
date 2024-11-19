namespace DataAccess.Entities
{
    public class ExportItem
    {
        public int Id { get; set; } // Primary Key
        public int DocumentId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal VatRate { get; set; }
        public string? ItemType { get; set; }
        public string? Unit { get; set; }
        public decimal? VatAmount { get; set; }
        public AccountingSettingsElement? AccountingSettings { get; set; }
    }
}
