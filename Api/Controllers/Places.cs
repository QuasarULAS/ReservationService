using Application.Services.PlaceHandler;
using Application.Services.PlaceHandler.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultHelper;

namespace Api.Controllers;

[Authorize]
[Route("[controller]")]
public class Places : Controller
{

    private IMediator _mediator;

    public Places(Mediator mediator)
    {
        _mediator = mediator;
    }

    ///<summary>جستوجوی پیشرفته مکان ها</summary>
    [HttpPost("[action]")]
    public async Task<ActionResult<ApiResult<SearchPlacesWithTotalAndListVM>>> SearchPlaces([FromBody] SearchPlaceRequest data)
    {
        var result = await _mediator.Send(data);
        if (result.IsSuccess) return Ok(value: result);
        else return BadRequest(error: result);
    }

    ///<summary>ایجاد مکان</summary>
    [HttpPost("[action]")]
    public async Task<ActionResult<ApiResult<bool>>> InsertPlace([FromBody] InsertPlaceRequest data)
    {
        var result = await _mediator.Send(data);
        if (result.IsSuccess) return Ok(value: result);
        else return BadRequest(error: result);
    }

    ///<summary>آپدیت مکان</summary>
    [HttpPut("[action]")]
    public async Task<ActionResult<ApiResult<bool>>> UpdatePlace([FromBody] UpdatePlaceRequest data)
    {
        var result = await _mediator.Send(data);
        if (result.IsSuccess) return Ok(value: result);
        else return BadRequest(error: result);
    }

    ///<summary>حذف مکان</summary>
    [HttpDelete("[action]/{placeId:int}")]
    public async Task<ActionResult<ApiResult<bool>>> DeletePlace(DeletePlaceRequest param)
    {
        var result = await _mediator.Send(param);
        if (result.IsSuccess) return Ok(value: result);
        else return BadRequest(error: result);
    }
}