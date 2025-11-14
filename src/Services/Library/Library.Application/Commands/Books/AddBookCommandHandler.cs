using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Commands.Books
{
    public class AddBookCommandHandler : ICommandHandler<AddBookCommand, Result<Guid>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork) 
        { 
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Author, request.Pages, request.Isbn, request.TotalCopies);

            await _bookRepository.AddAsync(book, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(book.Id);
        }
    }
}
