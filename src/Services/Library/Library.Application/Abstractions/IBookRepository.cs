
using Library.Domain.Entities;

namespace Library.Application.Abstractions
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken =default);
        Task AddAsync(Book book,CancellationToken cancellationToken = default);
        Task UpdateAsync(Book book,CancellationToken cancellationToken =default);
    }
}
