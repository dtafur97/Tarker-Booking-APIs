using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarket.Booking.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> test()
        {
            var number = int.Parse("test");
            return StatusCode(StatusCodes.Status200OK, 
                ResponseApiService.Response(StatusCodes.Status200OK, "Ejecución exitosa", "{}"));
        }
    }
}
