# Tarker.Booking

Tarker.Booking is a .NET 8.0 based application for managing bookings, users, and customers. This project is structured into multiple layers to promote separation of concerns and maintainability. Using Clean Architecture and CQRS

## Project Structure

- **Tarker.Booking.API**: The main entry point for the application, containing the API controllers and configuration.
- **Tarker.Booking.Application**: Contains the application logic, including commands, queries, and services.
- **Tarker.Booking.Common**: Contains common utilities and shared code.
- **Tarker.Booking.Domain**: Contains the domain entities and business logic.
- **Tarker.Booking.External**: Contains external integrations and services.
- **Tarker.Booking.Persistence**: Contains the database context and data access logic.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:
   git clone https://github.com/your-repo/tarker.booking.git
   cd tarker.booking

2. Restore the dependencies:
   dotnet restore

3. Update the connection string in `appsettings.json`:
	{
	  "ConnectionStrings": {
		"SQLConnectionString": "Your SQL Server connection string here"
	  }
	}

4. Apply the database migrations:
   dotnet ef database update --project src/Tarker.Booking.Persistence


### Running the Application

1. Navigate to the API project directory:
   cd src/Tarker.Booking.API
   
2. Run the application:
   dotnet run

## Project References

- **Tarker.Booking.API**:
    - `..\Tarker.Booking.Application\Tarker.Booking.Application.csproj`
    - `..\Tarker.Booking.Common\Tarker.Booking.Common.csproj`
    - `..\Tarker.Booking.External\Tarker.Booking.External.csproj`
    - `..\Tarker.Booking.Persistence\Tarker.Booking.Persistence.csproj`

- **Tarker.Booking.Application**:
    - `..\Tarker.Booking.Domain\Tarker.Booking.Domain.csproj`

- **Tarker.Booking.External**:
    - `..\Tarker.Booking.Application\Tarker.Booking.Application.csproj`
	
## Dependencies

- **Tarker.Booking.API**:
    - Azure.Extensions.AspNetCore.Configuration.Secrets
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools
    - Swashbuckle.AspNetCore

- **Tarker.Booking.Application**:
    - AutoMapper
    - FluentValidation
    - Microsoft.AspNetCore.Mvc.Abstractions
    - Microsoft.AspNetCore.Mvc.Core
    - Microsoft.EntityFrameworkCore.SqlServer

- **Tarker.Booking.External**:
    - Microsoft.ApplicationInsights.AspNetCore
    - Microsoft.AspNetCore.Authentication.JwtBearer
    - Microsoft.Extensions.Configuration.Abstractions
    - Microsoft.Extensions.DependencyInjection.Abstractions
    - System.IdentityModel.Tokens.Jwt