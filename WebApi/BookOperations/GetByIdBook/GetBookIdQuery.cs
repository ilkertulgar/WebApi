﻿using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetByIdBook;

public class GetBookIdQuery
{
    private readonly BookStoreDbContext _context;

    public GetBookIdQuery(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public BooksViewModel Handle(int id)
    {
        var            book = _context.Books!.Where(book => book.Id == id).SingleOrDefault();
        BooksViewModel vm   = new BooksViewModel();
        vm.Id          = book!.Id;
        vm.Title       = book.Title;
        vm.Genre       = ((GenreEnum)book.GenreId).ToString();
        vm.PublishDate = book.PublishDate.ToString("MM/dd/yyyy");
        vm.PageCount   = book.PageCount;
        return vm;
    }

    public class BooksViewModel
    {
        public int     Id        { get; set; }
        public string? Title     { get; set; }
        public int     PageCount { get; set; }

        public String? PublishDate { get; set; }

        public string? Genre { get; set; }
    }
}