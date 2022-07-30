using Microsoft.EntityFrameworkCore;


namespace WebApi.DBOperations;

public static class DataGenerator
{
    public static void Initialized(IServiceProvider serviceProvider)
    {
        var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());

        if (context.Books!.Any())
        {
            return;
        }

        context.Books!.AddRange(new Book()
                                {
                                  //  Id          = 1,
                                    Title       = "Learn Starup",
                                    GenreId     = 1,
                                    PageCount   = 200,
                                    PublishDate = new DateTime(2022, 06, 01)
                                },
                                new Book()
                                {
                                  //  Id          = 2,
                                    Title       = "Commet Lake",
                                    GenreId     = 2,
                                    PageCount   = 100,
                                    PublishDate = new DateTime(2022, 05, 10)
                                },
                                new Book()
                                {
                                   // Id          = 3,
                                    Title       = "Learn Starup",
                                    GenreId     = 2,
                                    PageCount   = 541,
                                    PublishDate = new DateTime(2002, 10, 21)
                                });
        context.SaveChanges();
    }
}