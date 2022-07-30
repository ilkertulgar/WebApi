using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook;

public class UpdateBookId
{
    private readonly BookStoreDbContext _context;

    public UpdateBookId(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public int Handle(int id, Book updateBook)
    {
        var book = _context.Books!.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return 0;
        }
        else
        {
            book.GenreId     = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount   = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title       = updateBook.Title != default ? updateBook.Title : book.Title;
            _context.SaveChanges();
            return 1;
        }
    }
}