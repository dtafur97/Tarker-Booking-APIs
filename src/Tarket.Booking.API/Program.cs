using Microsoft.VisualBasic;
using Tarker.Booking.Application;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
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


var app = builder.Build();

app.MapPost("/testService", async (IUpdateUserPasswordCommand service) =>
{
    var model = new UpdateUserPasswordModel
    {
        UserId = 1,
        Password = "Password3"
    };

    return await service.Execute(model);
});

app.Run();

