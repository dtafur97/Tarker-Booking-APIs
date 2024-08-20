using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;
using Tarket.Booking.API;

var builder = WebApplication.CreateBuilder(args);

// Add dependencies injection services
builder.Services
    .AddWebApi()
    .AddCommon()
    .AddAplication()
    .AddExternal(builder.Configuration)
    .AddPersistence(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();

