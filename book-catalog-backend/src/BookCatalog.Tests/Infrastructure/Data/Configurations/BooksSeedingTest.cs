using BookCatalog.Domain.Enums;
using BookCatalog.Infrastructure.Data.Configurations;
using FluentAssertions;

namespace BookCatalog.Tests.Infrastructure.Data.Configurations;

[TestFixture]
public class BooksSeedingTest
{
    [Test]
    public void GenerateBooks_ShouldReturnAllBooks()
    {
        // Act
        var books = BooksSeeding.GenerateBooks();

        // Assert
        books.Should().NotBeNull();
        books.Should().HaveCount(50); 
    }

    [Test]
    public void GenerateBooks_ShouldPopulateAllProperties()
    {
        // Act
        var books = BooksSeeding.GenerateBooks();

        // Assert
        books.Should().OnlyContain(b =>
            !string.IsNullOrWhiteSpace(b.ISBN) &&
            !string.IsNullOrWhiteSpace(b.Title) &&
            !string.IsNullOrWhiteSpace(b.Author) &&
            b.Id != Guid.Empty &&
            b.CreatedAt <= DateTime.UtcNow
        );
    }

    [Test]
    public void GenerateBooks_ShouldGenerateUniqueIds()
    {
        // Act
        var books = BooksSeeding.GenerateBooks();

        // Assert
        books.Select(b => b.Id).Should().OnlyHaveUniqueItems();
    }

    [Test]
    public void GenerateBooks_ShouldGenerateValidIsbnFormat()
    {
        // Act
        var books = BooksSeeding.GenerateBooks();

        // Assert
        books.Should().OnlyContain(b =>
            b.ISBN.StartsWith("978-0-") &&
            b.ISBN.Length >= 10
        );
    }

    [Test]
    public void GenerateBooks_ShouldDistributeOwnershipValues()
    {
        // Act
        var books = BooksSeeding.GenerateBooks();

        // Assert
        var ownershipValues = Enum.GetValues<Ownership>();

        books.Select(b => b.Ownership)
            .Distinct()
            .Should().BeEquivalentTo(ownershipValues);
    }
}