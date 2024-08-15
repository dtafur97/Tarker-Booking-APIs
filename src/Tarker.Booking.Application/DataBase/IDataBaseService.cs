using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Domain.Entities.Booking;
using Tarker.Booking.Domain.Entities.Customer;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.DataBase
{
    public interface IDataBaseService
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<BookingEntity> Booking { get; set; }
        Task<bool> SaveAsync();
    }
}
