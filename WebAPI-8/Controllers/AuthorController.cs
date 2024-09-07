using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI_8.DTO.Author;
using WebAPI_8.Models;
using WebAPI_8.Services.Author;

namespace WebAPI_8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(IAuthorInterface authorInterface) : Controller
{
   [HttpGet("GetAuthors")]
   public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> GetAuthors()
   {
      var authors = await authorInterface.ListAuthors();

      return Ok(authors);
   }

   [HttpGet("GetAuthorById/{authorId:int}")]
   public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorById(int authorId)
   {
      var author = await authorInterface.FindAuthorById(authorId);

      return Ok(author);
   }

   [HttpGet("GetAuthorByBookId/{bookId:int}")]
   public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorByBookId(int bookId)
   {
      var author = await authorInterface.FindAuthorByBookId(bookId);

      return Ok(author);
   }

   [HttpPost("CreateAuthor")]
   public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> CreateAuthor(CreateAuthorDto dto)
   {
      var allAuthors = await authorInterface.CreateAuthor(dto);

      return Created("", allAuthors);
   }
   
   [HttpPut("UpdateAuthor")]
   public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> UpdateAuthor(UpdateAuthorDto dto)
   {
      var allAuthors = await authorInterface.UpdateAuthor(dto);

      return Ok(allAuthors);
   }
   
   [HttpDelete("DeleteAuthor/{authorId:int}")]
   public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> DeleteAuthor(int authorId)
   {
      var allAuthors = await authorInterface.DeleteAuthor(authorId);

      return Ok(allAuthors);
   }
}