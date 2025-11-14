using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Abstractions
{
    public interface ILendingRecordRepository
    {
        Task AddLendingRecordAsync(LendingRecord record, CancellationToken cancellationToken =default);
        Task<IReadOnlyList<LendingRecord>> GétByBookIdAsync(Guid bookId, CancellationToken cancellationToken =default);
        Task<IReadOnlyList<LendingRecord>> GetByBorrowerIdAsync(Guid borrowerId, CancellationToken cancellationToken = default);

    }
}
