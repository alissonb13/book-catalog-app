using BookCatalog.Api.DTOs;
using BookCatalog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SearchBooksResponseDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchBooks([FromQuery] SearchBooksRequestDTO request)
    {
        return Ok(
            SearchBooksResponseDTO.From(
                await bookService.SearchBooks(
                    request.ToSearchBooksParamsDTO()
                )
            )
        );
    }
}