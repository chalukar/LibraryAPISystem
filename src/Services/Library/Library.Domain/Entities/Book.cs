

namespace Library.Domain.Entities
{
    public class Book
    {
        private Book() { } // EF

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; } = default!;
        public string Author { get; private set; } = default!;
        public string Isbn { get; private set; } = default!;
        public int Pages { get; private set; }
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }

        public ICollection<LendingRecord> LendingRecord { get; private set; } = new List<LendingRecord>();

        public Book(string title, string author, string isbn, int pages, int totalCopies)
        {
            if (totalCopies < 0) throw new ArgumentException("Total copies cannot be negative.");
            if (pages <= 0) throw new ArgumentException("Pages must be positive.");

            Title = title;
            Author = author;
            Isbn = isbn;
            Pages = pages;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public void Update(string title, string author, string isbn, int pages, int totalCopies)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            Pages = pages;
            if (totalCopies < TotalCopies - AvailableCopies)
                throw new InvalidOperationException("Cannot reduce total copies below borrowed count.");

            AvailableCopies += (totalCopies - TotalCopies);
            TotalCopies = totalCopies;
        }

        public void Borrow()
        {
            if (AvailableCopies <= 0)
                throw new InvalidOperationException("No copies available.");
            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies >= TotalCopies)
                throw new InvalidOperationException("All copies already returned.");
            AvailableCopies++;
        }
    }
}
