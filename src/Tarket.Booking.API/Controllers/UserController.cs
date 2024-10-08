﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.DeleteUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUsernameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Application.External.GetTokenJwt;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Common.Constants;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarket.Booking.API.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Authorize]
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : ControllerBase
    {
        private readonly IInsertAplicationInsightsService _insertAplicationInsightsService;

        public UserController(IInsertAplicationInsightsService insertAplicationInsightsService)
        {
            _insertAplicationInsightsService = insertAplicationInsightsService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model, [FromServices] ICreateUserCommand createUserCommand,
            [FromServices] IValidator<CreateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await createUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserModel model, [FromServices] IUpdateUserCommand updateUserCommand,
            [FromServices] IValidator<UpdateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await updateUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordModel model, [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand,
            [FromServices] IValidator<UpdateUserPasswordModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await updateUserPasswordCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> Delete(int userId, [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            if (userId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await deleteUserCommand.Execute(userId);

            if (!data)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllUserQuery getAllUserQuery)
        {

            var metric = new InsertApplicationInsightsModel(
                ApplicationInsightsConstants.METRIC_TYPE_API_CALL,
                EntitiesConstants.USER,
                "get-all");

            _insertAplicationInsightsService.Execute(metric);

            var data = await getAllUserQuery.Execute();

            if (data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-id/{userId}")]
        public async Task<IActionResult> GetById(int userId, [FromServices] IGetUserByIdQuery getUserByIdQuery)
        {
            var metric = new InsertApplicationInsightsModel(
                ApplicationInsightsConstants.METRIC_TYPE_API_CALL,
                EntitiesConstants.USER,
                "get-by-id");

            _insertAplicationInsightsService.Execute(metric);

            if (userId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getUserByIdQuery.Execute(userId);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [AllowAnonymous]
        [HttpGet("get-by-username-password/{userName}/{password}")]
        public async Task<IActionResult> GetByUsernamePassword(string userName, string password, [FromServices] IGetUserByUsernameAndPasswordQuery getUserByUsernameAndPasswordQuery,
            [FromServices] IValidator<(string, string)> validator,
            [FromServices] IGetTokenJwtService getTokenJwtService)
        {
            var metric = new InsertApplicationInsightsModel(
                ApplicationInsightsConstants.METRIC_TYPE_API_CALL,
                EntitiesConstants.USER,
                "get-by-username-password");

            _insertAplicationInsightsService.Execute(metric);

            var validate = await validator.ValidateAsync((userName, password));

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await getUserByUsernameAndPasswordQuery.Execute(userName, password);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            data.Token = getTokenJwtService.Execute(data.UserId.ToString());

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
