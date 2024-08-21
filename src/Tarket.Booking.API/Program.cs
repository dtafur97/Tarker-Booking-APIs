using Azure.Identity;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;
using Tarket.Booking.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Obtain KeyVault
var keyVaultUrl = builder.Configuration["keyVaultUrl"] ?? string.Empty;

//Local
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
{
    string tenantId = Environment.GetEnvironmentVariable("tenantId") ?? string.Empty;
    string clienteId = Environment.GetEnvironmentVariable("clientId") ?? string.Empty;
    string clienteSecret = Environment.GetEnvironmentVariable("clientSecret") ?? string.Empty;

    var tokenCredentials = new ClientSecretCredential(tenantId, clienteId, clienteSecret);
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), tokenCredentials);
}
else
{ 
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());
}

// Add dependencies injection services
builder.Services
    .AddWebApi()
    .AddCommon()
    .AddAplication()
    .AddExternal(builder.Configuration)
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarker Booking API V1");
    options.RoutePrefix = string.Empty;
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

