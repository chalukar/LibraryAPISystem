using AutoMapper;
using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Domain.Repositories;

namespace Library.Application.Handlers.CommandHandlers
{
    public class UpdateBookHandler : ICommandHandler<UpdateBookCommand, Result<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existing = await _bookRepository.GetByIdAsync(request.Book.Id, cancellationToken);
            if (existing is null)
                return Result<BookDto>.Failure("Book not found.");

            existing.Update(request.Book.Title, request.Book.Author, request.Book.Isbn,
                request.Book.Pages, request.Book.TotalCopies);

            await _bookRepository.UpdateAsync(existing, cancellationToken);

            return Result<BookDto>.Success(_mapper.Map<BookDto>(existing));
        }
    }
}
