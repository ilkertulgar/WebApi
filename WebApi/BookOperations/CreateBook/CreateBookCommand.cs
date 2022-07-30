using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    public           CreateBookModel    Model { get; set; }
    private readonly BookStoreDbContext _context;
    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public void Handle()
    {
        var book = _context.Books!.SingleOrDefault(x => x.Title == Model.Title);
        if (book is not null)
        {
            throw new InvalidCastException("Kitap Mevcut");
        }
        else
        {
            book             = new Book();
            book.Title       = Model.Title;
            book.PublishDate = Model.PublisDate;
            book.PageCount   = Model.PageCount;
            book.GenreId     = Model.GenraId;
            book.PublishDate=Model.PublisDate;

            _context.Books!.Add(book);
            _context.SaveChanges();
        }
    }
    public class CreateBookModel 
    {
        public String Title{ get; set; }
        
        public int GenraId { get; set; }
        
        public int PageCount { get; set; }
        
        public DateTime PublisDate { get; set; }
    }
}