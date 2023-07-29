using Application.Services.BookingHandler;
using Infrastructure.Repositories.BookingRepo.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultHelper;

namespace Api.Controllers
{

    [Authorize]
    [Route("[controller]")]
    public class Booking : Controller
    {
        private IMediator _mediator;

        public Booking(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<ApiResult<List<BookLogVM>>>> GetAllBookLogDetails()
        {
            var result = await _mediator.Send(new GetAllReservationRequest());
            if (result.IsSuccess) return Ok(value: result);
            else return BadRequest(error: result);
        }

        ///<summary>رزرو مکان</summary>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ApiResult<bool>>> ReservePlace([FromBody] MakeReservationRequest data)
        {
            var result = await _mediator.Send(data);
            if (result.IsSuccess) return Ok(value: result);
            else return BadRequest(error: result);
        }
    }
}