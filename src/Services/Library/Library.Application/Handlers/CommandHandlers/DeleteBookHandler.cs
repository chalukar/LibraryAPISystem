using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Commands;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.CommandHandlers
{
    public class DeleteBookHandler : ICommandHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var existing = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existing is null)
                return Result.Failure("Book not found.");

            await _bookRepository.DeleteAsync(existing, cancellationToken);
            return Result.Success();
        }
    }
}
