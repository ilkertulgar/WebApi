using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetByIdBook;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    private readonly BookStoreDbContext _context;


    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query  = new GetBooksQuery(_context);
        var           result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBookIdQuery query  = new GetBookIdQuery(_context);
        var            result = query.Handle(id);
        return Ok(result);
    }


    [HttpPost]
    public IActionResult AddBook([FromBody]CreateBookCommand.CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context);

        try
        {
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody]Book updateBook)
    {
        var book = _context.Books!.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        book.GenreId     = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
        book.PageCount   = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
        book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
        book.Title       = updateBook.Title != default ? updateBook.Title : book.Title;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookId bookId = new DeleteBookId(_context);

        var result = bookId.Handle(id);

        if (result == 0)
        {
            return BadRequest("Kayıtlı Kitap Bulunamadı");
        }

        return Ok("Kitap Silinmiştir.");
    }
}