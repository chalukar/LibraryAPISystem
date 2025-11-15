using BuildingBlocks.CQRS;
using Library.Application.Queries;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Library.Application.Handlers.QueryHandlers
{
    public class GetUserReadingPaceHandler : IQueryHandler<GetUserReadingPaceQuery, UserReadingPaceDto?>
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;

        public GetUserReadingPaceHandler(ILendingRecordRepository lendingRecordRepository)
        {
            _lendingRecordRepository =  lendingRecordRepository;
        }
        public async Task<UserReadingPaceDto?> Handle(GetUserReadingPaceQuery request, CancellationToken cancellationToken)
        {
            var lendings = await _lendingRecordRepository.Query()
            .Where(l => l.BorrowerId == request.BorrowerId && l.ReturnedAt != null)
            .ToListAsync(cancellationToken);

            if (!lendings.Any()) return null;

            var totalPages = lendings.Sum(l => l.Book.Pages);
            var totalDays = lendings.Sum(l => l.GetReadingDays() ?? 0.0);

            if (totalDays <= 0) return null;

            var pace = totalPages / totalDays;
            return new UserReadingPaceDto(request.BorrowerId, pace);
        }
    }
}
