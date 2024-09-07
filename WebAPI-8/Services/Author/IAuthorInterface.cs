using WebAPI_8.DTO.Author;
using WebAPI_8.Models;

namespace WebAPI_8.Services.Author;

public interface IAuthorInterface
{
    Task<ResponseModel<List<AuthorModel>>> ListAuthors();
    Task<ResponseModel<AuthorModel>> FindAuthorById(int authorId);
    Task<ResponseModel<AuthorModel>> FindAuthorByBookId(int bookId);
    Task<ResponseModel<List<AuthorModel>>> CreateAuthor(CreateAuthorDto dto);
    Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDto dto);
    Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int authorId);

}