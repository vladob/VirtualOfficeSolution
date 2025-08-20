namespace DataAccess.Services;

public sealed record PostImportResult(
    int DeletedDuplicates,
    int PopulatedAdresar,
    int ConvertedCount1,
    int ConvertedCount2,
    string? ExportedXml // null if not produced
);

public interface IPostImportProcedures
{
    Task<PostImportResult> RunAsync(
        string companyId,
        DateTime exportFrom,
        CancellationToken ct = default);
}
