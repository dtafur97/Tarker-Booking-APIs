﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tarker.Booking.Application.Configuration;
using Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBookings;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByType;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomers;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.DeleteUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUsernameAndPassword;

namespace Tarker.Booking.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            #region User
            services.AddSingleton(mapper.CreateMapper());
            services.AddTransient<ICreateUserCommand, CreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
            services.AddTransient<IUpdateUserPasswordCommand, UpdateUserPasswordCommand>();

            services.AddTransient<IGetAllUserQuery, GetAllUserQuery>();
            services.AddTransient<IGetUserByIdQuery, GetUserByIdQuery>();
            services.AddTransient<IGetUserByUsernameAndPasswordQuery, GetUserByUsernameAndPasswordQuery>();
            #endregion

            #region Customer
            services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddTransient<IUpdateCustomerCommand, UpdateCustomerCommand>();
            services.AddTransient<IDeleteCustomerCommand, DeleteCustomerCommand>();
            services.AddTransient<IGetAllCustomerQuery, GetAllCustomerQuery>();
            services.AddTransient<IGetCustomerByIdQuery, GetCustomerByIdQuery>();
            services.AddTransient<IGetCustomerByDocumentNumberQuery, GetCustomerByDocumentNumberQuery>();
            #endregion

            #region Booking
            services.AddTransient<ICreateBookingCommand, CreateBookingCommand>();
            services.AddTransient<IGetAllBookingQuery, GetAllBookingQuery>();
            services.AddTransient<IGetBookingsByDocumentNumberQuery, GetBookingsByDocumentNumberQuery>();
            services.AddTransient<IGetBookingsByTypeQuery, GetBookingsByTypeQuery>();
            #endregion

            return services;
        }

    }
}
