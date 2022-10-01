using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.BuildServiceProvider();
// Add services to the container.

builder.Services.AddControllers();
//Inmemory database
builder.Services.AddEntityFrameworkInMemoryDatabase();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDb"));
DataGenerator.Initialized(builder.Services.BuildServiceProvider());
//Inmemory database

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// Middleware işlemleri
//app.Run(async contex=> Console.WriteLine("Middlware 1."));

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 1 başladı.");
//     await next.Invoke();
//     Console.WriteLine("Middleware 1 sonlandırılıyor.");
// });
//
// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 2 başladı.");
//     await next.Invoke();
//     Console.WriteLine("Middleware 2 sonlandırılıyor.");
// });
//
// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 3 başladı.");
//     await next.Invoke();
//     Console.WriteLine("Middleware 3 sonlandırılıyor.");
// });

// app.Map("/example", interalApp => interalApp.Run(async context =>
// {
//     Console.WriteLine("/example middleware tetiklendi");
//     await context.Response.WriteAsync("/example middleware tetiklendi");
// }));

//app.MapWhen(x => x.Request.Method == "GET", internalApp => { internalApp.Run(async context => { app.MapWhen(x => x.Request.Method == "GET", internalApp => { Console.WriteLine("MapWhen Tetiklendi."); }); }); });

app.MapControllers();
app.Run();