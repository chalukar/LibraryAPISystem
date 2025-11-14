
using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; } = default!;
        public string Author { get; private set; } = default!;
        public int Pages { get; private set; }
        public string Isbn { get; private set; } = default!;
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }

        public Book(string title, string author, int pages, string isbn, int totalCopies)
        {
            if (totalCopies < 0) throw new DomainException("Total copies cannot be negative.");

            Title = title;
            Author = author;
            Pages = pages;
            Isbn = isbn;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public void Borrow()
        {
            if (AvailableCopies <= 0)
                throw new DomainException("No copies available to borrow.");

            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies >= TotalCopies)
                throw new DomainException("All copies are already returned.");

            AvailableCopies++;
        }
    }
}
