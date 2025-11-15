
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowerRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Borrower borrower, CancellationToken cancellationToken = default)
        {
            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Borrower>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Borrower?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers.FindAsync(new object?[] { id }, cancellationToken);
        }
    }
}
