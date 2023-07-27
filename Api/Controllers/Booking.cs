using Infrastructure.Repositories.BookingRepo;
using Infrastructure.Repositories.BookingRepo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("[controller]")]
public class Booking : Controller
{
    [HttpGet]
    [Route("[action]")]
    public List<BookLogVM> GetAllBookLogDetails()
    {
        var list = BookingRepository.BookLog();
        return list;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult ReservePlace([FromBody] MakeBookLogWithoutUserIdDto bookLogDetails)
    {
        var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ID");
        var isReserved = BookingRepository.InsertBookLog(bookLogDetails, id.Value);
        //if (isReserved == 0) return Ok("Successful!");
        //if (isReserved == 2) return BadRequest("This Place Is Reserved By Another Person In This Time.");
        //return BadRequest("bad request");
    }
}