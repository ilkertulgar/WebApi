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

app.MapControllers();
app.Run();