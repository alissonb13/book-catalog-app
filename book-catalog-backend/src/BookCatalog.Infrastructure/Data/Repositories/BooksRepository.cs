using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;
using BookCatalog.Domain.Repositories;
using BookCatalog.Infrastructure.Data.Configurations;
using BookCatalog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Infrastructure.Data.Repositories;

public class BooksRepository(BookCatalogDbContext context) : IBooksRepository
{
    public async Task<PaginatedSearchResultDTO<Book>> SearchAsync(SearchBooksParamsDTO dto)
    {
        var sortField = dto.SortBy?.ToString() ?? nameof(Book.CreatedAt);
        var ascending = dto.SortOrder == SortOrderField.Asc;

        var query = context.Books.AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(dto.Author), x => x.Author.ToUpper().Contains(dto.Author!.ToUpper()))
            .WhereIf(!string.IsNullOrWhiteSpace(dto.ISBN), x => x.ISBN == dto.ISBN)
            .WhereIf(dto.Ownership.HasValue, x => x.Ownership == dto.Ownership)
            .OrderByDynamic(sortField, ascending);

        var total = await query.CountAsync();
        var books = await query
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();

        return new PaginatedSearchResultDTO<Book>(books, dto.PageSize, total, dto.Page);
    }
}