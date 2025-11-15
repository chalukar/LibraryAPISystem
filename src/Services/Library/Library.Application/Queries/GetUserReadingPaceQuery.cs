using BuildingBlocks.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries
{
    public record UserReadingPaceDto(Guid BorrowerId, double PagesPerDay);

    public record GetUserReadingPaceQuery(Guid BorrowerId) : IQuery<UserReadingPaceDto?>;
}
