using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public record UpdateBookDto(Guid Id,string Title,string Author,string Isbn,int Pages,int TotalCopies);
}
