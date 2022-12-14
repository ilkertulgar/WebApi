using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    public           CreateBookModel    Model { get; set; } = null!;
    private readonly BookStoreDbContext _context;
    private readonly IMapper            _mapper;
    public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
    {
        _context = dbContext;
        _mapper  = mapper;
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
            book = _mapper.Map<Book>(Model);//new Book();
            //book.Title       = Model.Title;
            //book.PublishDate = Model.PublisDate;
            //book.PageCount   = Model.PageCount;
            //book.GenreId     = Model.GenraId;
            //book.PublishDate=Model.PublisDate;

            _context.Books!.Add(book);
            _context.SaveChanges();
        }
    }
    public class CreateBookModel 
    {
        public string? Title { get; set; }
        
        public int GenreId { get; set; }
        
        public int PageCount { get; set; }
        
        public DateTime PublishDate { get; set; }
     
    }
}