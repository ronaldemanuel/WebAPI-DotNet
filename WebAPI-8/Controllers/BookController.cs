using Microsoft.AspNetCore.Mvc;
using WebAPI_8.DTO.Book;
using WebAPI_8.Models;
using WebAPI_8.Services.Book;

namespace WebAPI_8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IBookInterface bookInterface) : Controller
{
    [HttpGet("GetBooks")]
    public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBooks()
    {
        var books = await bookInterface.ListBooks();

        return Ok(books);
    }

    [HttpGet("GetBookById/{bookId:int}")]
    public async Task<ActionResult<ResponseModel<BookModel>>> GetBookById(int bookId)
    {
        var book = await bookInterface.FindBookById(bookId);
        
        return Ok(book);
    }
    
    [HttpGet("GetBooksByAuthorId/{authorId:int}")]
    public async Task<ActionResult<ResponseModel<BookModel>>> GetBookByAuthorId(int authorId)
    {
        var books = await bookInterface.FindBooksByAuthorId(authorId);
        
        return Ok(books);
    }
    
    [HttpGet("GetBooksByTitle/{title}")]
    public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBookByTitle(string title)
    {
        var books = await bookInterface.FindBooksByTitle(title);
        
        return Ok(books);
    }
    
    [HttpPost("CreateBook")]
    public async Task<ActionResult<ResponseModel<List<BookModel>>>> CreateBook(CreateBookDto dto)
    {
        var books = await bookInterface.CreateBook(dto);
        
        return Created("", books);
    }
    
    [HttpPut("UpdateBook")]
    public async Task<ActionResult<ResponseModel<List<BookModel>>>> UpdateBook(UpdateBookDto dto)
    {
        var books = await bookInterface.UpdateBook(dto);
        
        return Ok(books);
    }
    
    [HttpDelete("DeleteBook/{bookId:int}")]
    public async Task<ActionResult<ResponseModel<List<BookModel>>>> DeleteBook(int bookId)
    {
        var books = await bookInterface.DeleteBook(bookId);
        
        return Ok(books);
    }
    
}