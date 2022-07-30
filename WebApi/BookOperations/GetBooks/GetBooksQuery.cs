using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _context;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public List<BooksViewModel> Handle()
    {
        var                  booklist = _context.Books!.OrderBy(x => x.Id).ToList<Book>();
        List<BooksViewModel> vm       = new List<BooksViewModel>();

        foreach (var book in booklist)
        {
            vm.Add(new BooksViewModel()
                   {
                       Id          = book.Id,
                       Title       = book.Title,
                       Genre       = ((GenreEnum)book.GenreId).ToString(),
                       PublishDate = book.PublishDate.ToString("MM/dd/yyyy"),
                       PageCount   = book.PageCount
                   });
        }

        return vm;
    }

    public class BooksViewModel
    {
        public int    Id          { get; set; }
        public string Title       { get; set; }
        public int    PageCount   { get; set; }
        public String PublishDate { get; set; }
        public string Genre       { get; set; }
    }
}