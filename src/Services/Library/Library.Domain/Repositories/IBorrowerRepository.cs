using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IBorrowerRepository
    {
        Task<Borrower?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Borrower>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Borrower borrower, CancellationToken cancellationToken = default);
    }
}
