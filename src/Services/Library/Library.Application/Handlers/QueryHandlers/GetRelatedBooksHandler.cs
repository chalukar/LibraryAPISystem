using AutoMapper;
using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using Library.Application.Queries;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Handlers.QueryHandlers
{
    public class GetRelatedBooksHandler : IQueryHandler<GetRelatedBooksQuery, IReadOnlyList<BookDto>>
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;
        private readonly IMapper _mapper;

        public GetRelatedBooksHandler(ILendingRecordRepository lendingRecordRepository, IMapper mapper)
        {
            _lendingRecordRepository = lendingRecordRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BookDto>> Handle(GetRelatedBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _lendingRecordRepository.Query();

            var borrowers = await query
                .Where(l => l.BookId == request.BookId)
                .Select(l => l.BorrowerId)
                .Distinct().ToListAsync(cancellationToken);

            if (!borrowers.Any()) return Array.Empty<BookDto>();

            var relatedBooks = await query
                .Where(l => borrowers.Contains(l.BorrowerId) && l.BookId != request.BookId)
                .Select(l => l.Book)
                .Distinct()
                .Take(request.Top)
                .ToListAsync(cancellationToken);

            return relatedBooks.Select(b => _mapper.Map<BookDto>(b)).ToList();
        }
    }
}
