using System.ComponentModel.DataAnnotations;
using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Enums;

namespace BookCatalog.Api.DTOs;

public record SearchBooksRequestDTO(
    [MaxLength(13, ErrorMessage = "ISBN must have maximum 13 characters.")]
    string? ISBN,
    [MaxLength(255, ErrorMessage = "Author name must have maximum 255 characters.")]
    string? Author,
    [EnumDataType(typeof(Ownership), ErrorMessage = "Ownership type mismatch.")]
    Ownership? Ownership,
    [EnumDataType(typeof(BookSortField), ErrorMessage = "Sort field mismatch.")]
    BookSortField? SortBy,
    [EnumDataType(typeof(SortOrderField), ErrorMessage = "Order field mismatch.")]
    SortOrderField SortOrder = SortOrderField.Asc,
    [Range(1, 100, ErrorMessage = "Page size must be between 10 and 100.")]
    int PageSize = 10,
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater then 1.")]
    int Page = 1
)
{
    public SearchBooksParamsDTO ToSearchBooksParamsDTO()
    {
        return new SearchBooksParamsDTO(ISBN, Author, Ownership, SortBy, SortOrder, PageSize, Page);
    }
}