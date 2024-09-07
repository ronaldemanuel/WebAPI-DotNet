using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_8.Models;

public class BookModel
{
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
    public AuthorModel Author { get; set; }
}