using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class LendingRecord
    {
        private LendingRecord() { }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid BookId { get; private set; }
        public Guid BorrowerId { get; private set; }
        public DateTime BorrowedAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }

        public Book Book { get; private set; } = default!;
        public Borrower Borrower { get; private set; } = default!;

        public LendingRecord(Book book, Borrower borrower)
        {
            Book = book;
            Borrower = borrower;
            BookId = book.Id;
            BorrowerId = borrower.Id;
            BorrowedAt = DateTime.UtcNow;
        }

        public void Return()
        {
            if (ReturnedAt != null)
                throw new InvalidOperationException("Already returned.");

            ReturnedAt = DateTime.UtcNow;
        }

        public double? GetReadingDays()
        {
            if (ReturnedAt == null) return null;
            return (ReturnedAt.Value - BorrowedAt).TotalDays;
        }
    }
}
