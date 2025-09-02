using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;

namespace BookCatalog.Api.DTOs;

public record SearchBooksResponseDTO(List<BookResponseDTO> Books, int Page, int PageSize, int Total)
{
    public static SearchBooksResponseDTO From(PaginatedSearchResultDTO<Book> result)
    {
        return new SearchBooksResponseDTO(
            BookResponseDTO.MapFromEntityList(result.Items.ToList()),
            result.Page,
            result.PageSize,
            result.Total
        );
    }
}

public record BookResponseDTO(
    string ISBN,
    string Title,
    string Author,
    Ownership Ownership,
    DateTime CreatedAt)
{
    private static BookResponseDTO FromEntity(Book book)
    {
        return new BookResponseDTO(
            book.ISBN,
            book.Title,
            book.Author,
            book.Ownership,
            book.CreatedAt
        );
    }

    public static List<BookResponseDTO> MapFromEntityList(List<Book> books)
    {
        return books.Select(FromEntity).ToList();
    }
}