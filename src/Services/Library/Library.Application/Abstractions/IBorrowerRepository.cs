using Library.Domain.Entities;

namespace Library.Application.Abstractions
{
    public interface IBorrowerRepository
    {
        Task<Borrower?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Borrower borrower, CancellationToken cancellationToken = default);
    }
}
