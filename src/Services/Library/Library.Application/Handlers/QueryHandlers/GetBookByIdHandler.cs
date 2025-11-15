using AutoMapper;
using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using Library.Application.Queries;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.QueryHandlers
{
    public class GetBookByIdHandler : IQueryHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            return book is null ? null : _mapper.Map<BookDto>(book);
        }
    }
}
