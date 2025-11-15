using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Commands;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.CommandHandlers
{
    public class ReturnBookHandler : ICommandHandler<ReturnBookCommand, Result>
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;
        private readonly IBookRepository _bookRepository;

        public ReturnBookHandler(ILendingRecordRepository lendingRecordRepository, IBookRepository bookRepository)
        {
            _lendingRecordRepository = lendingRecordRepository;
            _bookRepository = bookRepository;
        }
        public async Task<Result> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var lending = await _lendingRecordRepository.GetByIdAsync(request.LendingId, cancellationToken);
            if (lending is null) return Result.Failure("Lending record not found.");

            var book = await _bookRepository.GetByIdAsync(lending.BookId, cancellationToken);
            if (book is null) return Result.Failure("Book not found.");

            try
            {
                lending.Return();
                book.Return();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

            await _lendingRecordRepository.SaveChangesAsync(cancellationToken);
            await _bookRepository.UpdateAsync(book, cancellationToken);

            return Result.Success();
        }
    }
}
