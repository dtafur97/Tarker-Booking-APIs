using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomers;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarket.Booking.API.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class CustomerController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerModel createCustomerModel, [FromServices] ICreateCustomerCommand createCustomerCommand,
            [FromServices] IValidator<CreateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(createCustomerModel);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await createCustomerCommand.Execute(createCustomerModel);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerModel updateCustomerModel, [FromServices] IUpdateCustomerCommand updateCustomerCommand,
            [FromServices] IValidator<UpdateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(updateCustomerModel);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors.Select(x => x.ErrorMessage).ToList()));

            var data = await updateCustomerCommand.Execute(updateCustomerModel);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> Delete(int customerId, [FromServices] IDeleteCustomerCommand deleteCustomerCommand)
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await deleteCustomerCommand.Execute(customerId);

            if (!data)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCustomerQuery getAllCustomerQuery)
        {
            var data = await getAllCustomerQuery.Execute();

            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-id/{customerId}")]
        public async Task<IActionResult> GetById(int customerId, [FromServices] IGetCustomerByIdQuery getCustomerByIdQuery)
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByIdQuery.Execute(customerId);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-documentNumber/{documentNumber}")]
        public async Task<IActionResult> GetByDocumentNumber(string documentNumber, [FromServices] IGetCustomerByDocumentNumberQuery getCustomerByDocumentNumberQuery)
        {
            if (string.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByDocumentNumberQuery.Execute(documentNumber);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
