using Library.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowersController : Controller
    {
        private readonly IMediator _mediator;
        public BorrowersController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet("top")]
        public async Task<IActionResult> TopBorrowers([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int top = 10)
        {
            var result = await _mediator.Send(new GetTopBorrowersQuery(from, to, top));
            return Ok(result);
        }

        [HttpGet("{borrowerId:guid}/reading-pace")]
        public async Task<IActionResult> ReadingPace(Guid borrowerId)
        {
            var result = await _mediator.Send(new GetUserReadingPaceQuery(borrowerId));
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
