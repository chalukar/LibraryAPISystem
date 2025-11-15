using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public record UpdateBookCommand(UpdateBookDto Book) : ICommand<Result<BookDto>>;
}
