using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Interfaces.Services;

public interface IBookService
{
    Task<PaginatedSearchResultDTO<Book>> SearchBooks(SearchBooksParamsDTO dto);
}