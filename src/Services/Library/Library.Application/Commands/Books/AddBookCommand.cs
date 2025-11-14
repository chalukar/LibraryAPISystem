using BuildingBlocks.Common;
using BuildingBlocks.CQRS;

namespace Library.Application.Commands.Books
{
    public record AddBookCommand(string Title,string Author,int Pages,string Isbn,int TotalCopies) : ICommand<Result<Guid>>;
}
