using Microsoft.EntityFrameworkCore;
using WebAPI_8.Data;
using WebAPI_8.DTO.Author;
using WebAPI_8.Models;

namespace WebAPI_8.Services.Author;

public class AuthorService(AppDbContext context) : IAuthorInterface
{
    public async Task<ResponseModel<List<AuthorModel>>> ListAuthors()
    {
        var response = new ResponseModel<List<AuthorModel>>();
        try
        {
            var authors = await context.Authors.ToListAsync();
            response.Data = authors;
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine("OPa");
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> FindAuthorById(int authorId)
    {
        var response = new ResponseModel<AuthorModel>();
        
        try
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);

            if (author == null)
            {
                response.Message = "No Author Found";
                return response;
            }
            
            response.Data = author;
            
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> FindAuthorByBookId(int bookId)
    {
       var response = new ResponseModel<AuthorModel>();
        
        try
        {
            var book = await context.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                response.Message = "No Book Found";
                
                return response;
            }
            
            response.Data = book.Author;
            response.Message = "Author Found";

            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<AuthorModel>>> CreateAuthor(CreateAuthorDto dto)
    {
        var response = new ResponseModel<List<AuthorModel>>();
        
        try
        {
            var author = new AuthorModel()
            {
                Name = dto.Name,
                LastName = dto.LastName
            };

            context.Add(author);
            await context.SaveChangesAsync();
            
            response.Data = await context.Authors.ToListAsync();
            response.Message = "Author Created";

            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDto dto)
    {
        var response = new ResponseModel<List<AuthorModel>>();
        
        try
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == dto.Id);

            if (author == null)
            {
                response.Message = "No Author Found";
                
                return response;
            }

            author.Name = dto.Name;
            author.LastName = dto.LastName;
            
            context.Update(author);
            await context.SaveChangesAsync();
            
            response.Data = await context.Authors.ToListAsync();
            response.Message = "Author Updated";

            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            
            return response;
        }
    }

    public async Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int authorId)
    {
        var response = new ResponseModel<List<AuthorModel>>();
        
        try
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
            
            if (author == null)
            {
                response.Message = "No Author Found";
                
                return response;
            }

            context.Remove(author);
            await context.SaveChangesAsync();
            
            response.Data = await context.Authors.ToListAsync(); 
            response.Message = "Author Deleted";
            
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