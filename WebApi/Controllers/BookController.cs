using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetByIdBook;
using WebApi.BookOperations.UpdateBook;
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
        UpdateBookId bookId = new UpdateBookId(_context);
       var result =  bookId.Handle(id, updateBook);

       if (result==0)
       {
           return BadRequest("Güncelleme Yapılamamıştır.");
       }
        return Ok("Güncelleme İşlemi Başarılı.");
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