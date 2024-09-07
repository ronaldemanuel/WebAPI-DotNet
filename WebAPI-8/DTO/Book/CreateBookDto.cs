using WebAPI_8.Services.Book.Link;

namespace WebAPI_8.DTO.Book;

public class CreateBookDto
{
    public required string Title { get; set; }
    public required AuthorLinkDto Author { get; set; }
}