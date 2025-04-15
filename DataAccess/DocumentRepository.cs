using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DocumentRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

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