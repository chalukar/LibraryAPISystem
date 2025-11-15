using BuildingBlocks.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries
{
    public record TopBorrowerDto(Guid BorrowerId, string Name, string Email, int TotalBorrowed);

    public record GetTopBorrowersQuery(DateTime From, DateTime To, int Top = 10)
        : IQuery<IReadOnlyList<TopBorrowerDto>>;
}
