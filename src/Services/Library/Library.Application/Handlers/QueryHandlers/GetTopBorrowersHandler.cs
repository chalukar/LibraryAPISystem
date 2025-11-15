using BuildingBlocks.CQRS;
using Library.Application.Queries;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Library.Application.Handlers.QueryHandlers
{
    public class GetTopBorrowersHandler : IQueryHandler<GetTopBorrowersQuery, IReadOnlyList<TopBorrowerDto>>
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;

        public GetTopBorrowersHandler(ILendingRecordRepository lendingRecordRepository)
        {
            _lendingRecordRepository =  lendingRecordRepository;
        }
        public async Task<IReadOnlyList<TopBorrowerDto>> Handle(GetTopBorrowersQuery request, CancellationToken cancellationToken)
        {
            var query = _lendingRecordRepository.Query()
            .Where(l => l.BorrowedAt >= request.From && l.BorrowedAt <= request.To);

            var result = await query
                .GroupBy(l => l.Borrower)
                .OrderByDescending(g => g.Count())
                .Take(request.Top)
                .Select(g => new TopBorrowerDto(
                    g.Key.Id,
                    g.Key.Name,
                    g.Key.Email,
                    g.Count())).ToListAsync(cancellationToken);

            return result;
        }
    }
}
