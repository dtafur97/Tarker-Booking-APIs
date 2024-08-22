using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.SendGridEmail;
using Tarker.Booking.Domain.Models.SendGridEmail;
using Tarker.Booking.Application.Features;

namespace Tarket.Booking.API.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/v1/notification")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class NotificationController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SendGridEmailRequestModel model, [FromServices] ISendGridEmailService sendGridEmailService)
        {
            var data = await sendGridEmailService.Execute(model);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseApiService.Response(StatusCodes.Status500InternalServerError));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK));
        }
    }
}
