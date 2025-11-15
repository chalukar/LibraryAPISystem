using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto?>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _mediator.Send(new CreateBookCommand(dto));
            if (!result.IsSuccess) return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateBookDto dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch.");

            var result = await _mediator.Send(new UpdateBookCommand(dto));
            if (!result.IsSuccess) return NotFound(result.Error);
            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));
            if (!result.IsSuccess) return NotFound(result.Error);
            return NoContent();
        }

        [HttpGet("most-borrowed")]
        public async Task<IActionResult> MostBorrowed([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int top = 10)
        {
            var result = await _mediator.Send(new GetMostBorrowedBooksQuery(from, to, top));
            return Ok(result);
        }

        [HttpGet("{id:guid}/related")]
        public async Task<IActionResult> Related(Guid id, [FromQuery] int top = 10)
        {
            var result = await _mediator.Send(new GetRelatedBooksQuery(id, top));
            return Ok(result);
        }
    }
}
