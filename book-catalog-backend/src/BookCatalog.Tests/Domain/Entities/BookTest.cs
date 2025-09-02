using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;
using FluentAssertions;

namespace BookCatalog.Tests.Domain.Entities;

[TestFixture]
public class BookTest
{
    [Test]
    public void Constructor_ShouldInitializeProperties_WhenParametersAreValid()
    {
        // Arrange
        const string isbn = "1234567890123";
        const string title = "Clean Code";
        const string author = "Robert C. Martin";
        const Ownership ownership = Ownership.OWN;

        // Act
        var book = new Book(isbn, title, author, ownership);

        // Assert
        book.Id.Should().NotBeEmpty();
        book.ISBN.Should().Be(isbn);
        book.Title.Should().Be(title);
        book.Author.Should().Be(author);
        book.Ownership.Should().Be(ownership);
        book.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void Constructor_ShouldThrowException_WhenIsbnIsNullOrEmpty()
    {
        Action act1 = () => new Book(null!, "Title", "Author", Ownership.OWN);
        Action act2 = () => new Book("", "Title", "Author", Ownership.OWN);

        act1.Should().Throw<ArgumentNullException>();
        act2.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_ShouldThrowException_WhenTitleIsNullOrEmpty()
    {
        Action act1 = () => new Book("1234567890123", null!, "Author", Ownership.OWN);
        Action act2 = () => new Book("1234567890123", "", "Author", Ownership.OWN);

        act1.Should().Throw<ArgumentNullException>();
        act2.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_ShouldThrowException_WhenAuthorIsNullOrEmpty()
    {
        Action act1 = () => new Book("1234567890123", "Title", null!, Ownership.OWN);
        Action act2 = () => new Book("1234567890123", "Title", "", Ownership.OWN);

        act1.Should().Throw<ArgumentNullException>();
        act2.Should().Throw<ArgumentException>();
    }
}