using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DocumentRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<int> SaveBatchAndCreateLogAsync(
            string companyCin,
            DateTime? exportFrom,
            IEnumerable<Document> documents,
            CancellationToken ct = default)
        {
            // Defensive null handling
            var docs = documents?.ToList() ?? new List<Document>();

            // Counts from the in-memory batch
            var docsCount = docs.Count;
            var itemsCount = docs.Sum(d => d.Items?.Count ?? 0);
            var vatSumsCount = docs.Sum(d => d.VatSummary?.Count ?? 0);

            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            // Persist documents (+ related entities if attached to navs)
            _context.Documents.AddRange(docs);

            // If you sometimes attach Items/VatSummary separately (not via navs),
            // you can also explicitly add them to ensure they’re tracked:
            // _context.Items.AddRange(docs.SelectMany(d => d.Items ?? Enumerable.Empty<ExportItem>()));
            // _context.VatSummarys.AddRange(docs.SelectMany(d => d.VatSummary ?? Enumerable.Empty<CustomVatInterface>()));

            await _context.SaveChangesAsync(ct);

            // Create initial log row
            var log = new ImportLog
            {
                CompanyCin = companyCin,
                ExecutionDate = DateTime.UtcNow,
                ExportFrom = exportFrom?.Date,
                DokladoDocumentsCount = docsCount,
                DokladoItemsCount = itemsCount,
                DokladoVatSummariesCount = vatSumsCount
            };

            _context.ImportLogs.Add(log);
            await _context.SaveChangesAsync(ct);

            await tx.CommitAsync(ct);
            return log.Id;
        }

        public async Task SaveDocumentsAsync(IEnumerable<Entities.Document> documents)
        {
            _context.Documents.AddRange(documents);
            await _context.SaveChangesAsync();
        }
        public async Task SaveAttachmentsAsync(IEnumerable<Entities.Attachment> attachments)
        {
            _context.Attachments.AddRange(attachments);
            await _context.SaveChangesAsync();
        }

        public List<string> GetDocumentErpIdsFromDatabase()
        {
            List<string> documentErpIds = _context.Documents.FromSqlRaw(@"
                SELECT d.[DocumentErpId] 
                FROM [dbo].[Documents] d
                LEFT JOIN [dbo].[Attachments] a ON a.[DocumentId] = d.[DocumentErpId]
                WHERE a.[Id] IS NULL").Select(d => d.DocumentErpId).ToList();

            return documentErpIds;
        }
    }
}