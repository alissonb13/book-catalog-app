using AutoFixture;
using BookCatalog.Domain.DTOs;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;
using BookCatalog.Domain.Interfaces.Services;
using BookCatalog.Domain.Repositories;
using BookCatalog.Domain.Services;
using FluentAssertions;
using Moq;

namespace BookCatalog.Tests.Domain.Services;

[TestFixture]
public class BookServiceTest
{
    private Mock<IBooksRepository> _booksRepositoryMock;
    private IBookService _bookService;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _booksRepositoryMock = new Mock<IBooksRepository>();
        _bookService = new BookService(_booksRepositoryMock.Object);
    }

    private static IEnumerable<TestCaseData> SearchBooksTestCases()
    {
        yield return new TestCaseData(
            new Book[] { new Book("1234567890123", "Clean Code", "Robert C. Martin", Ownership.OWN) }, 1
        ).SetName("ReturnsOneBook");

        yield return new TestCaseData(
            Array.Empty<Book>(), 0
        ).SetName("ReturnsEmpty");
    }

    [Test]
    [TestCaseSource(nameof(SearchBooksTestCases))]
    public async Task SearchBooks_ShouldReturnExpectedResult(Book[] items, int total)
    {
        // Arrange
        var dto = _fixture.Create<SearchBooksParamsDTO>();
        var expected = new PaginatedSearchResultDTO<Book>(
            Items: items,
            PageSize: 10,
            Total: total,
            Page: 1
        );

        _booksRepositoryMock
            .Setup(r => r.SearchAsync(dto))
            .ReturnsAsync(expected);

        // Act
        var result = await _bookService.SearchBooks(dto);

        // Assert
        result.Should().NotBeNull();
        result.Total.Should().Be(total);
        result.Items.Should().HaveCount(total);

        _booksRepositoryMock.Verify(r => r.SearchAsync(dto), Times.Once);
    }
    
    [Test]
    public void SearchBooks_ShouldPropagateException_WhenRepositoryThrows()
    {
        // Arrange
        var dto = _fixture.Create<SearchBooksParamsDTO>();
        _booksRepositoryMock
            .Setup(r => r.SearchAsync(dto))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        // Act
        Func<Task> act = async () => await _bookService.SearchBooks(dto);

        // Assert
        act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("DB error");

        _booksRepositoryMock.Verify(r => r.SearchAsync(dto), Times.Once);
    }
}