using BookCatalog.Domain.Enums;

namespace BookCatalog.Domain.DTOs;

public record SearchBooksParamsDTO(
    string? ISBN,
    string? Author,
    Ownership? Ownership,
    BookSortField? SortBy,
    SortOrderField? SortOrder,
    int PageSize = 10,
    int Page = 1
);