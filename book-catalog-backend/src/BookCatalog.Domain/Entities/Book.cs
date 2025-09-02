using BookCatalog.Domain.Enums;

namespace BookCatalog.Domain.Entities;

public class Book
{
    protected Book()
    {
    }

    public Book(string isbn, string title, string author, Ownership ownership)
    {
        Validate(
            (isbn, nameof(ISBN)),
            (title, nameof(Title)),
            (author, nameof(Author))
        );

        Id = Guid.NewGuid();
        ISBN = isbn;
        Title = title;
        Author = author;
        Ownership = ownership;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string ISBN { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
    public Ownership Ownership { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private static void Validate(params (string? Value, string Field)[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value.Value))
            {
                throw new ArgumentNullException($"{value.Field} must not be null or empty");
            }
        }
    }
}