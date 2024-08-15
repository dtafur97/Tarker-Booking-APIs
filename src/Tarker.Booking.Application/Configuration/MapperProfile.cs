using AutoMapper;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUsernameAndPassword;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Configuration
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, CreateUserModel>().ReverseMap();
            CreateMap<UserEntity, UpdateUserModel>().ReverseMap();

            CreateMap<UserEntity, GetAllUserModel>().ReverseMap();
            CreateMap<UserEntity, GetUserByIdModel>().ReverseMap();
            CreateMap<UserEntity, GetUserByUsernameAndPasswordModel>().ReverseMap();
        }
    }
}
