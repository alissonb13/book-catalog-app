using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data.Configurations;
using BookCatalog.Infrastructure.Extensions;
using FluentAssertions;

namespace BookCatalog.Tests.Infrastructure.Extensions;

[TestFixture]
public class IQueryableExtensionsTest
{
    private List<Book> _books;

    [SetUp]
    public void SetUp()
    {
        _books = BooksSeeding.GenerateBooks();
    }

    #region WhereIf Tests

    [Test]
    public void WhereIf_ShouldFilter_WhenConditionIsTrue()
    {
        // Arrange
        const string author = "Robert";

        // Act
        var result = _books.AsQueryable()
            .WhereIf(true, x => x.Author.Contains(author))
            .ToList();

        // Assert
        result.Should().OnlyContain(x => x.Author.Contains(author));
    }

    [Test]
    public void WhereIf_ShouldNotFilter_WhenConditionIsFalse()
    {
        // Act
        var result = _books.AsQueryable()
            .WhereIf(false, x => x.ISBN.StartsWith("123"))
            .ToList();

        // Assert
        result.Should().HaveCount(_books.Count);
    }

    #endregion

    #region OrderByDynamic Tests

    [Test]
    public void OrderByDynamic_ShouldOrderByTitleAscending()
    {
        // Act
        var result = _books.AsQueryable()
            .OrderByDynamic(nameof(Book.Title), ascending: true)
            .ToList();

        // Assert
        var sorted = _books.Select(b => b.Title).OrderBy(t => t).ToList();
        result.Select(b => b.Title).Should().Equal(sorted);
    }

    [Test]
    public void OrderByDynamic_ShouldOrderByTitleDescending()
    {
        // Act
        var result = _books.AsQueryable()
            .OrderByDynamic(nameof(Book.Title), ascending: false)
            .ToList();

        // Assert
        var sorted = _books.Select(b => b.Title).OrderByDescending(t => t).ToList();
        result.Select(b => b.Title).Should().Equal(sorted);
    }

    [Test]
    public void OrderByDynamic_ShouldReturnOriginal_WhenPropertyIsNullOrEmpty()
    {
        // Act
        var result1 = _books.AsQueryable()
            .OrderByDynamic("", ascending: true)
            .ToList();

        var result2 = _books.AsQueryable()
            .OrderByDynamic(null!, ascending: true)
            .ToList();

        // Assert
        result1.Should().Equal(_books);
        result2.Should().Equal(_books);
    }

    [Test]
    public void OrderByDynamic_ShouldThrow_WhenPropertyDoesNotExist()
    {
        // Act
        Action act = () => _books.AsQueryable().OrderByDynamic("NonExistent");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    #endregion
}