namespace DataAccess.Entities;

public class ImportLog
{
    public int Id { get; set; }
    public string CompanyCin { get; set; } = null!;
    public DateTime ExecutionDate { get; set; }        // set in code to UtcNow
    public DateTime? ExportFrom { get; set; }

    public int? DokladoDocumentsCount { get; set; }
    public int? DokladoItemsCount { get; set; }
    public int? DokladoVatSummariesCount { get; set; }

    public int? DeletedDuplicates { get; set; }
    public int? MkSoftAdresarCount { get; set; }
    public int? MkSoftDokladyCount { get; set; }
    public int? MkSoftPohybyCount { get; set; }
}
