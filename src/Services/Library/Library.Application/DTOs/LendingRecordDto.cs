using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public record LendingRecordDto(Guid Id, Guid BookId, Guid BorrowerId,DateTime BorrowedAt, DateTime? ReturnedAt);
}
