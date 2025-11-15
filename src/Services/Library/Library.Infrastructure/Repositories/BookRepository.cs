
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Book book, CancellationToken cancellationToken = default)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Books.FindAsync(new object?[] { id }, cancellationToken);
        }

        public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
