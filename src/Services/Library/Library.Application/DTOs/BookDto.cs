using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public record BookDto(Guid Id, string Title, string Author, string Isbn, int Pages, int TotalCopies, int AvailableCopies);
}
