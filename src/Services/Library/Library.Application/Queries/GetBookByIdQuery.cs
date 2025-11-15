using BuildingBlocks.CQRS;
using Library.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries
{
    public record GetBookByIdQuery(Guid Id) : IQuery<BookDto?>;
}
