using BuildingBlocks.Common;
using BuildingBlocks.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public record DeleteBookCommand(Guid Id) : ICommand<Result>;
}
