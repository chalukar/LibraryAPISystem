using Grpc.Core;
using Library.Application.DTOs;
using Library.Application.Queries;
using MediatR;

namespace Library.gRPC.Services
{
    public class LibraryGrpcService : LibraryService.LibraryServiceBase
    {
        private readonly IMediator _mediator;

        public LibraryGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAllBooksResponse> GetAllBooks(Empty request, ServerCallContext context)
        {
            var books = await _mediator.Send(new GetAllBooksQuery(), context.CancellationToken);
            var response = new GetAllBooksResponse();
            response.Books.AddRange(books.Select(MapBook));
            return response;
        }

        public override async Task<BookResponse> GetBookById(BookRequest request, ServerCallContext context)
        {
            var id = Guid.Parse(request.Id);
            var book = await _mediator.Send(new GetBookByIdQuery(id), context.CancellationToken);
            if (book is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));

            return MapBook(book);
        }

        private static BookResponse MapBook(BookDto book) => new()
        {
            Id = book.Id.ToString(),
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Pages = book.Pages,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies
        };
    }
}
