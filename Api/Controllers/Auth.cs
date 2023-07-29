using Application.Services.AuthHandler;
using Infrastructure.Repositories.AuthRepo.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultHelper;

namespace Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private IMediator _mediator;

        public Auth(IMediator mediator)
        {
            _mediator = mediator;
        }

        // LoginWithUserPassword
        [AllowAnonymous]
        ///<summary>دریافت توکن</summary>
        [HttpPost]
        [Route("Authenticate")]
        public async Task<ActionResult<ApiResult<Tokens>>> Authenticate(AuthenticateRequest data)
        {
            var result = await _mediator.Send(data);
            if (result.IsSuccess) return Ok(value: result);
            else return BadRequest(error: result);
        }

        // Registration
        [AllowAnonymous]
        ///<summary>ثبت کاربر</summary>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResult<bool>>> RegisterUser([FromBody] RegisterUserRequest data)
        {
            var result = await _mediator.Send(data);
            if (result.IsSuccess) return Ok(value: result);
            else return BadRequest(error: result);
        }
    }
}