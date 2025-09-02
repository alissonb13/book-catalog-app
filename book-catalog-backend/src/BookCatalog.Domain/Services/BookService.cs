using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Interfaces.Services;
using BookCatalog.Domain.Repositories;

namespace BookCatalog.Domain.Services;

public class BookService(IBooksRepository booksRepository) : IBookService
{
    public async Task<PaginatedSearchResultDTO<Book>> SearchBooks(SearchBooksParamsDTO dto)
    {
        return await booksRepository.SearchAsync(dto);
    }
}