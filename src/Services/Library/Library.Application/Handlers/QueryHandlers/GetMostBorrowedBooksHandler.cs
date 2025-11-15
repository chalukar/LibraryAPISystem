using AutoMapper;
using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using Library.Application.Queries;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Handlers.QueryHandlers
{
    public class GetMostBorrowedBooksHandler : IQueryHandler<GetMostBorrowedBooksQuery, IReadOnlyList<BookDto>>
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;
        private readonly IMapper _mapper;

        public GetMostBorrowedBooksHandler(ILendingRecordRepository lendingRecordRepository, IMapper mapper)
        {
            _lendingRecordRepository = lendingRecordRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BookDto>> Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _lendingRecordRepository .Query();

            if (request.From.HasValue)
                query = query.Where(l => l.BorrowedAt >= request.From.Value);

            if (request.To.HasValue)
                query = query.Where(l => l.BorrowedAt <= request.To.Value);

            var result = await query
                .GroupBy(l => l.Book)
                .OrderByDescending(g => g.Count())
                .Take(request.Top)
                .Select(g => g.Key).ToListAsync(cancellationToken);

            return result.Select(b => _mapper.Map<BookDto>(b)).ToList();
        }
    }
}
