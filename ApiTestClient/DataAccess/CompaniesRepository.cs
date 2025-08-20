

using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{

    public class CompaniesRepository
    {
        private AppDbContext _context;

        public CompaniesRepository(AppDbContext context)
        {
            this._context = context;
        }

        public object GetCompanies()
        {
            return _context.Set<CompanyLookup>()
                .FromSqlRaw("SELECT [Name], [ICO] FROM [VirtualOfficeDB].[MkSoft].[Company] ORDER BY [Name]")
                .ToList();
        }
    }
}