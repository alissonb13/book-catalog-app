namespace BookCatalog.Domain.DTOs;

public record PaginatedSearchResultDTO<T>(
    IReadOnlyList<T> Items,
    int PageSize,
    int Total,
    int Page
);