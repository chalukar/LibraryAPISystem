using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.Commands;
using Library.Domain.Entities;
using Library.Domain.Repositories;


namespace Library.Application.Handlers.CommandHandlers
{
    public class BorrowBookHandler : ICommandHandler<BorrowBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly ILendingRecordRepository _lendingRecordRepository;

        public BorrowBookHandler(IBookRepository bookRepository,IBorrowerRepository borrowerRepository, ILendingRecordRepository lendingRecordRepository)
        {
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
            _lendingRecordRepository = lendingRecordRepository;
        }
        public async Task<Result> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book is null) return Result.Failure("Book not found.");

            var borrower = await _borrowerRepository.GetByIdAsync(request.BorrowerId, cancellationToken);
            if (borrower is null) return Result.Failure("Borrower not found.");

            try
            {
                book.Borrow();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

            var lending = new LendingRecord(book, borrower);
            await _lendingRecordRepository.AddAsync(lending, cancellationToken);
            await _bookRepository.UpdateAsync(book, cancellationToken);

            return Result.Success();
        }
    }
}
