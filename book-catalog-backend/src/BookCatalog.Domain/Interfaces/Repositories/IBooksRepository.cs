using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Repositories;

public interface IBooksRepository
{
    public Task<PaginatedSearchResultDTO<Book>> SearchAsync(SearchBooksParamsDTO dto);
}