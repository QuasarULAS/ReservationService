using Infrastructure.Repositories.AuthRepo;
using Infrastructure.Repositories.AuthRepo.Model;
using Infrastructure.Repositories.UserRepo;
using Infrastructure.Repositories.UserRepo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJWTManagerRepository _jWTManager;

    public AuthController(IJWTManagerRepository jWTManager)
    {
        _jWTManager = jWTManager;
    }

    // LoginWithUserPassword
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public IActionResult Authenticate(UserAuthenticateM usersdata)
    {
        var token = _jWTManager.Authenticate(usersdata);

        if (token == null) return Unauthorized();

        return Ok(token);
    }

    // Registration
    [AllowAnonymous]
    [HttpPost("[action]")]
    public IActionResult RegisterUser([FromBody] InsertUserInputModel user)
    {
        var IsUserAdded = UserRepository.InsertUser(user);
        return IsUserAdded ? Ok("User Added Successfully.") : BadRequest("Bad Request!");
    }
}