using AutoMapper;
using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.CommandHandlers
{
    public class CreateBookHandler : ICommandHandler<CreateBookCommand, Result<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Book;
            var book = new Book(dto.Title, dto.Author, dto.Isbn, dto.Pages, dto.TotalCopies);

            await _bookRepository.AddAsync(book, cancellationToken);

            var result = _mapper.Map<BookDto>(book);
            return Result<BookDto>.Success(result);
        }
    }
}
