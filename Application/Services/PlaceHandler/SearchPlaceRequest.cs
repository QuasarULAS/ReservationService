using Infrastructure.Repositories.PlaceRepo.Model;
using MediatR;
using Infrastructure;
using ResultHelper;
using Application.Services.PlaceHandler.Dto;

namespace Application.Services.PlaceHandler
{

    public class SearchPlaceRequest : SearchPlacesIM, IRequest<ApiResult<SearchPlacesWithTotalAndListVM>>
    { }
    public class SearchPlaceRequestHandler : IRequestHandler<SearchPlaceRequest, ApiResult<SearchPlacesWithTotalAndListVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<SearchPlacesWithTotalAndListVM> _apiResult;

        public SearchPlaceRequestHandler(IUnitOfWork unitOfWork, IApiResult<SearchPlacesWithTotalAndListVM> apiResult)
        {
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<SearchPlacesWithTotalAndListVM>> Handle(SearchPlaceRequest request, CancellationToken cancellationToken)
        {
            SearchPlacesIM requestModel = new()
            {
                PlaceName = request.PlaceName,
                PlaceKind = request.PlaceKind,
                Page = request.Page,
                Limit = request.Limit,
            };

            var result = await _unitOfWork.Place.SearchPlaces(requestModel);
            if (result.Count == 0) return _apiResult.WithSuccess("no content");

            int total = result[0].Total;
            List<SearchPlacesWithoutTotalDto> _list = result.Select(x => new SearchPlacesWithoutTotalDto
            {
                PlaceID = x.PlaceID,
                PlaceName = x.PlaceName,
                Address = x.Address,
                GeographicalLocation = x.GeographicalLocation,
                BookingPerson = x.BookingPerson,
                PlaceKind = x.PlaceKind
            }).ToList();

            SearchPlacesWithTotalAndListVM allPlaces = new()
            {
                Total = total,
                data = _list
            };

            _apiResult.WithValue(allPlaces);
            return _apiResult.WithSuccess(EStatusCode.Success);
        }
    }
}


