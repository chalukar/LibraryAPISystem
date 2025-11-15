
using FluentAssertions;
using Library.Domain.Entities;

namespace Library.Tests.UnitTests.Domain
{
    public class BookTests
    {
        [Fact]
        public void CreateBook_WithValidParameters_ShouldCreateBook()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);

            book.Title.Should().Be("Pro C# 10");
            book.AvailableCopies.Should().Be(5);
        }

        [Fact]
        public void CreateBook_WithNegativePages_ShouldThrowArgumentException()
        {
            Action act = () => new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", -1353, 5);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Pages must be positive*");
        }

        [Fact]
        public void BorrowBook_WhenAvailableCopiesGreaterThanZero_ShouldDecreaseAvailableCopies()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            book.Borrow();
            book.AvailableCopies.Should().Be(4);
        }

        [Fact]
        public void BorrowBook_WhenNoAvailableCopies_ShouldThrowInvalidOperationException()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 1);
            book.Borrow();
            Action act = () => book.Borrow();
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("No copies available.");
        }

        [Fact]
        public void ReturnBook_WhenAvailableCopiesLessThanTotalCopies_ShouldIncreaseAvailableCopies()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            book.Borrow();
            book.Return();
            book.AvailableCopies.Should().Be(5);
        }
    }


}
