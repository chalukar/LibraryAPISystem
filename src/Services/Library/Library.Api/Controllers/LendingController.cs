using Library.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LendingController : Controller
    {
        private readonly IMediator _mediator;

        public LendingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow(Guid bookId, Guid borrowerId)
        {
            var result = await _mediator.Send(new BorrowBookCommand(bookId, borrowerId));
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok();
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return(Guid lendingId)
        {
            var result = await _mediator.Send(new ReturnBookCommand(lendingId));
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok();
        }
    }
}
