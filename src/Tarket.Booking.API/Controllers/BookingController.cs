using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBookings;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByType;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarket.Booking.API.Controllers
{
    [Route("api/v1/booking")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class BookingController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookingModel createBookingModel, [FromServices] ICreateBookingCommand createBookingCommand)
        {
            var data = await createBookingCommand.Execute(createBookingModel);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllBookingQuery getAllBookingQuery)
        {
            var data = await getAllBookingQuery.Execute();

            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        //With FromQuery
        [HttpGet("get-by-documentNumber")]
        public async Task<IActionResult> GetByDocumentNumber([FromQuery] string documentNumber, [FromServices] IGetBookingsByDocumentNumberQuery getBookingsByDocumentNumberQuery)
        {
            if (string.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getBookingsByDocumentNumberQuery.Execute(documentNumber);

            if (data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] string type, [FromServices] IGetBookingsByTypeQuery getBookingsByTypeQuery)
        {
            if (string.IsNullOrEmpty(type))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getBookingsByTypeQuery.Execute(type);

            if (data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
        
    }
}
