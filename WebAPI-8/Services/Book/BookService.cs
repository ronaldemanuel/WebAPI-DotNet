using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI_8.Data;
using WebAPI_8.DTO.Book;
using WebAPI_8.Models;

namespace WebAPI_8.Services.Book;

public class BookService(AppDbContext context) : IBookInterface
{
    public async Task<ResponseModel<List<BookModel>>> ListBooks()
    {
       var response = new ResponseModel<List<BookModel>>();
        
        try
        {
            response.Data = await context.Books.Include(bookModel => bookModel.Author).ToListAsync();
            response.Message = "Books Listed Successfully";

            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<BookModel>> FindBookById(int bookId)
    {
        var response = new ResponseModel<BookModel>();
        
        try
        {
            var book = await context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                response.Message = "Book Not Found";

                return response;
            }
            
            response.Data = book;
            response.Message = "Book Found";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<BookModel>>> FindBooksByAuthorId(int authorId)
    {
        var response = new ResponseModel<List<BookModel>>();
            
        try
        {
            var books = await context.Books
                .Include(b => b.Author)
                .Where(b => b.Author.Id == authorId)
                .ToListAsync();

            if (books.Count == 0)
            {
                response.Message = "No Books Found";
                
                return response;
            }
            
            response.Data = books;
            response.Message = "Book Found";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<BookModel>>> FindBooksByTitle(string title)
    {
        var response = new ResponseModel<List<BookModel>>();
        
        try
        {
            var lowerTitle = title.ToLower();

            var books = await context.Books
                .Where(b => 
                    EF.Functions.Like(b.Title.ToLower(), $"%{lowerTitle}%"))
                .ToListAsync();
            
            if (books.Count == 0)
            {
                response.Message = "No Books Found";
                
                return response;
            }
            
            response.Data = books;
            response.Message = "Book Found";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<BookModel>>> CreateBook(CreateBookDto dto)
    {
        var response = new ResponseModel<List<BookModel>>();
        
        try
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == dto.Author.Id);
            
            if (author == null)
            {
                response.Message = "Author Not Found";
                
                return response;
            }
            
            var newBook = new BookModel()
            {
                Title = dto.Title,
                Author = author
            };
            
            context.Books.Add(newBook);
            await context.SaveChangesAsync();
            
            response.Data = context.Books.ToList();
            response.Message = "Book Created";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<BookModel>>> UpdateBook(UpdateBookDto dto)
    {
        var response = new ResponseModel<List<BookModel>>();
        
        try
        {
            var book = await context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == dto.Id);
            
            if (book == null)
            {
                response.Message = "Book Not Found";
                return response;
            }
            
            book.Title = dto.Title ?? book.Title;

            if (dto.AuthorId != null)
            { 
                var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == dto.AuthorId);
                
                if (author == null)
                {
                    response.Message = "Author Not Found";
                    return response;
                }
                
                book.Author = author;
            }
            
            context.Books.Update(book);
            await context.SaveChangesAsync();
            
            response.Data = context.Books.ToList();
            response.Message = "Book Updated";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<BookModel>>> DeleteBook(int bookId)
    {
        var response = new ResponseModel<List<BookModel>>();
        
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                response.Message = "Book Not Found";

                return response;
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();
            
            response.Data = context.Books.ToList();
            response.Message = "Book Deleted";
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }
}