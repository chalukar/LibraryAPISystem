using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface ILendingRecordRepository
    {
        Task<LendingRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(LendingRecord lending, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        // For statistics:
        IQueryable<LendingRecord> Query();
    }
}
