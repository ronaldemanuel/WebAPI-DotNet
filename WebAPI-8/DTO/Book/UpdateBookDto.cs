namespace WebAPI_8.DTO.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
}