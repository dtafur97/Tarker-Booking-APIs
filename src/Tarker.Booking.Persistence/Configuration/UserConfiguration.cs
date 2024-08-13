using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Persistence.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserEntity> entityTypeBuilder) 
        {
            entityTypeBuilder.HasKey(x => x.UserId);
            entityTypeBuilder.Property(x => x.FirstName).IsRequired();
            entityTypeBuilder.Property(x => x.LastName).IsRequired();
            entityTypeBuilder.Property(x => x.UserName).IsRequired();
            entityTypeBuilder.Property(x => x.Password).IsRequired();

            entityTypeBuilder.HasMany(x => x.Bookings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
