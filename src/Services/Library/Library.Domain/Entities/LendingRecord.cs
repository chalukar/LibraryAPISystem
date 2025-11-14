using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class LendingRecord
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid BookId { get; private set; }
        public Guid BorrowerId { get; private set; }
        public DateTime BorrowedAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }

        public LendingRecord(Guid bookId, Guid borrowerId, DateTime borrowedAt)
        {
            BookId = bookId;
            BorrowerId = borrowerId;
            BorrowedAt = borrowedAt;
        }

        public void MarkReturned(DateTime returnedAt)
        {
            if (ReturnedAt is not null)
                throw new DomainException("This lending record is already returned.");

            if (returnedAt < BorrowedAt)
                throw new DomainException("Return date cannot be before borrow date.");

            ReturnedAt = returnedAt;
        }

        public int GetBorrowDurationDays()
        {
            var endDate = ReturnedAt ?? DateTime.UtcNow;
            return (endDate.Date - BorrowedAt.Date).Days + 1;
        }
    }
}
