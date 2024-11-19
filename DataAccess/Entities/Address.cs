namespace DataAccess.Entities
{
    public class Address
    {
        public int Id { get; set; } // Primary Key
        public int DocumentId { get; set; }
        public string? StreetName { get; set; }
        public string? PropertyRegistrationNumber { get; set; }
        public string? BuildingNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Municipality { get; set; }
        public string? Country { get; set; }
    }
}
