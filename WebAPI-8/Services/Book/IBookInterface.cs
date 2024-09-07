using WebAPI_8.DTO.Book;
using WebAPI_8.Models;

namespace WebAPI_8.Services.Book;

public interface IBookInterface
{
    Task<ResponseModel<List<BookModel>>> ListBooks();
    Task<ResponseModel<BookModel>> FindBookById(int bookId);
    Task<ResponseModel<List<BookModel>>> FindBooksByAuthorId(int authorId);
    Task<ResponseModel<List<BookModel>>> FindBooksByTitle(string title);
    Task<ResponseModel<List<BookModel>>> CreateBook(CreateBookDto dto);
    Task<ResponseModel<List<BookModel>>> UpdateBook(UpdateBookDto dto);
    Task<ResponseModel<List<BookModel>>> DeleteBook(int bookId);
}