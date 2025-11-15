using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class LendingRecordRepository : ILendingRecordRepository
    {
        private readonly LibraryDbContext _context;

        public LendingRecordRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LendingRecord lending, CancellationToken cancellationToken = default)
        {
            _context.LendingRecords.Add(lending);
            await _context.SaveChangesAsync(cancellationToken);
        }
              
        public async Task<LendingRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.LendingRecords.FindAsync(new object?[] { id }, cancellationToken);
        }

        public IQueryable<LendingRecord> Query()
        {
           return _context.LendingRecords
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .AsQueryable();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _context.SaveChangesAsync(cancellationToken);
        }
    }
}
