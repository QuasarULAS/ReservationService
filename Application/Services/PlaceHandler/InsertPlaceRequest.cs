using Infrastructure.Repositories.PlaceRepo.Model;
using MediatR;
using ResultHelper;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Application.Services.PlaceHandler;

public class InsertPlaceRequest : InsertPlaceWithoutUserIdIM, IRequest<ApiResult<bool>>
{
    public class InsertPlaceRequestHandler : IRequestHandler<InsertPlaceRequest, ApiResult<bool>>
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<bool> _apiResult;

        public InsertPlaceRequestHandler(HttpContext httpContext, IUnitOfWork unitOfWork, IApiResult<bool> apiResult)
        {
            _httpContext = httpContext;
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<bool>> Handle(InsertPlaceRequest request, CancellationToken cancellationToken)
        {
            string? claimId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == "ID")?.ToString();
            Guid UserId = new Guid(claimId);

            InsertPlaceWithoutUserIdIM requestModel = new()
            {
                Title = request.Title,
                Address = request.Address,
                PlaceTypeId = request.PlaceTypeId,
                GeographicalLocation = request.GeographicalLocation,
            };

            var result = await _unitOfWork.Place.InsertPlace(requestModel, UserId);
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess(EStatusCode.Success);
        }
    }
}


