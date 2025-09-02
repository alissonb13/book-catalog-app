using AutoFixture;
using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Enums;
using BookCatalog.Infrastructure.Data.Configurations;
using BookCatalog.Infrastructure.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Tests.Infrastructure.Data.Repositories;

[TestFixture]
public class BooksRepositoryTest
{
    private BookCatalogDbContext _context;
    private BooksRepository _repository;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<BookCatalogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .Options;

        _context = new BookCatalogDbContext(options);
        _repository = new BooksRepository(_context);
        _fixture = new Fixture();

        var books = BooksSeeding.GenerateBooks();
        _context.Books.AddRange(books);
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task SearchAsync_ShouldFilterByAuthor()
    {
        // Arrange
        const string author = "Robert C. Martin";
        var dto = new SearchBooksParamsDTO(
            ISBN: null,
            Author: author,
            Ownership: null,
            SortBy: null,
            SortOrder: null,
            PageSize: 10,
            Page: 1
        );

        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        result.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(b => b.Author.Contains(author));
    }

    [Test]
    public async Task SearchAsync_ShouldFilterByIsbn()
    {
        // Arrange
        var isbn = _context.Books.First().ISBN;

        var dto = new SearchBooksParamsDTO(
            ISBN: isbn,
            Author: null,
            Ownership: null,
            SortBy: null,
            SortOrder: null,
            PageSize: 10,
            Page: 1
        );
        
        Console.WriteLine(dto);

        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        result.Items.Should().ContainSingle();
        result.Items.First().ISBN.Should().Be(isbn);
    }

    [Test]
    public async Task SearchAsync_ShouldFilterByOwnership()
    {
        // Arrange
        const Ownership ownership = Ownership.OWN;

        var dto = _fixture.Build<SearchBooksParamsDTO>()
            .OmitAutoProperties()
            .With(x => x.Ownership, ownership)
            .With(x => x.Page, 1)
            .With(x => x.PageSize, 50)
            .Create();

        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        result.Items.Should().OnlyContain(b => b.Ownership == ownership);
    }

    [Test]
    public async Task SearchAsync_ShouldApplyPagination()
    {
        // Arrange
        const int page = 2;
        const int pageSize = 10;
        
        var dto = new SearchBooksParamsDTO(
            ISBN: null,
            Author: null,
            Ownership: null,
            SortBy: null,
            SortOrder: null,
            PageSize: pageSize,
            Page: page
        );
        
        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        result.Items.Should().HaveCount(pageSize);
        result.Total.Should().Be(_context.Books.Count());
    }

    [Test]
    public async Task SearchAsync_ShouldSortByTitleAscending()
    {
        // Arrange
        var dto = _fixture.Build<SearchBooksParamsDTO>()
            .OmitAutoProperties()
            .With(x => x.SortBy, BookSortField.Title)
            .With(x => x.SortOrder, SortOrderField.Asc)
            .With(x => x.Page, 1)
            .With(x => x.PageSize, 10)
            .Create();

        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        var sorted = result.Items
            .Select(b => b.Title)
            .OrderBy(t => t)
            .ToList();
        result.Items.Select(b => b.Title).Should().Equal(sorted);
    }

    [Test]
    public async Task SearchAsync_ShouldSortByTitleDescending()
    {
        // Arrange
        var dto = _fixture.Build<SearchBooksParamsDTO>()
            .OmitAutoProperties()
            .With(x => x.SortBy, BookSortField.Title)
            .With(x => x.SortOrder, SortOrderField.Desc)
            .With(x => x.Page, 1)
            .With(x => x.PageSize, 10)
            .Create();

        // Act
        var result = await _repository.SearchAsync(dto);

        // Assert
        var sorted = result.Items
            .Select(b => b.Title)
            .OrderByDescending(t => t)
            .ToList();
        result.Items.Select(b => b.Title).Should().Equal(sorted);
    }
}