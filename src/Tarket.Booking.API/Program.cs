using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Application.Interfaces;
using Tarker.Booking.Persistence.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataBaseService>(options =>
options.UseSqlServer(builder.Configuration["SQLConnectionString"]));

builder.Services.AddScoped<IDataBaseService, DataBaseService>();

var app = builder.Build();

//API Minimalist for test
app.MapPost("/createTest", async (IDataBaseService _dataBaseService) =>
{
    var entity = new Tarker.Booking.Domain.Entities.User.UserEntity
    {
        FirstName = "NameTest",
        LastName = "Tafur",
        UserName = "User01",
        Password = "123456"
     };

    await _dataBaseService.User.AddAsync(entity);
    await _dataBaseService.SaveAsync();

    return "User Created!";
});

app.MapGet("/testGet", async (IDataBaseService _dataBaseService) =>
{
    var users = await _dataBaseService.User.ToListAsync();
    return users;
});

app.Run();

