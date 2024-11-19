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
    }
}