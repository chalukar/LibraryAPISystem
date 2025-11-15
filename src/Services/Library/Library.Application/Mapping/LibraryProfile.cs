using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;


namespace Library.Application.Mapping
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Borrower, BorrowerDto>();
            CreateMap<LendingRecord, LendingRecordDto>();
        }
    }
}
