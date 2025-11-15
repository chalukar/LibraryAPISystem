using AutoMapper;
using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using Library.Application.Queries;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.QueryHandlers
{
    public class GetAllBooksHandler : IQueryHandler<GetAllBooksQuery, IReadOnlyList<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBooksHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            return books.Select(b => _mapper.Map<BookDto>(b)).ToList();
        }
    }
}
