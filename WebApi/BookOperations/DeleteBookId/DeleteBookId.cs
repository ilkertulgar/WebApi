using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBookId;

public class DeleteBookId
{
    private BookStoreDbContext _context;

    public DeleteBookId(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }
    public int Handle(int id)
    {
        var book = _context.Books!.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return 0;
        }
        _context.Books!.Remove(book);
        _context.SaveChanges();
        return 1;
    }
}