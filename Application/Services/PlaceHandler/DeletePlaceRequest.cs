using System.ComponentModel.DataAnnotations;
using Infrastructure;
using MediatR;
using ResultHelper;

namespace Application.Services.PlaceHandler;

public class DeletePlaceRequest : IRequest<ApiResult<bool>>
{
    [Required]
    public int PlaceId { get; set; }

    public class DeletePlaceRequestHandler : IRequestHandler<DeletePlaceRequest, ApiResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<bool> _apiResult;
        public DeletePlaceRequestHandler(IUnitOfWork unitOfWork, IApiResult<bool> apiResult)
        {
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<bool>> Handle(DeletePlaceRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.Place.GetPlaceById(request.PlaceId);
            if (isExist == null)
            {
                return _apiResult.WithSuccess(EStatusCode.NotFound);
            }
            var result = await _unitOfWork.Place.DeletePlace(request.PlaceId);
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess(EStatusCode.Success);
        }
    }
}