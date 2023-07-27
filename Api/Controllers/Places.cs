using Infrastructure.Repositories.PlaceRepo;
using Infrastructure.Repositories.PlaceRepo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("[controller]")]
public class Places : Controller
{
    [HttpPost("[action]")]
    public IActionResult SearchPlaces([FromBody] SearchPlacesDto spim)
    {
        var list = PlaceRepository.SearchPlaces(spim);

        object obj1 = new { err = "null" };
        object obj2 = new { err = "null pl" };
        object obj3 = new { err = "<0" };
        object obj4 = new { err = "none" };

        if (list.ToString() == obj1.ToString()) return BadRequest("Input must not be null!");
        if (list.ToString() == obj2.ToString()) return BadRequest("page and limit must not be null!");
        if (list.ToString() == obj3.ToString()) return BadRequest("page and limit must be grater than 0!");
        if (list.ToString() == obj4.ToString()) return NoContent();
        return Ok(list);
    }

    [HttpPost("[action]")]
    public IActionResult InsertPlace([FromBody] InsertPlaceWithoutUserIdDto place)
    {
        var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ID");
        var IsPlaceAdded = PlaceRepository.InsertPlace(place, id.Value);
        return IsPlaceAdded ? Ok("Place Added Successfully.") : BadRequest("Bad Request!");
    }

    [HttpPut("[action]")]
    public IActionResult UpdatePlace([FromBody] UpdatePlaceDto place)
    {
        var IsPlaceEdited = PlaceRepository.UpdatePlace(place);

        //if (IsPlaceEdited == 0) return Ok("Place edited successfully.");
        //if (IsPlaceEdited == 2) return NotFound("This place doesn't exist!");
        //return BadRequest("bad request!");
    }

    [HttpDelete("[action]/{placeId:int}")]
    public IActionResult DeletePlace(int placeId)
    {
        var _place = PlaceRepository.GetPlaceById(placeId);

        if (_place)
        {
            PlaceRepository.DeletePlace(placeId);
            return Ok("Place deleted successfully.")}
        else
        {
            return NotFound("This place doesn't exist!");
        }

        //if (IsPlaceDeleted == 0) return Ok("Place deleted successfully.");
        //if (IsPlaceDeleted == 2) return NotFound("This place doesn't exist!");
        //return BadRequest("bad request!");
    }
}