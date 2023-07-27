using Core.Models;
using Infrastructure.Repositories.PlaceRepo.Model;

namespace Infrastructure.Repositories.PlaceRepo;

public interface IPlaceRepository
{
    Task<List<SearchPlacesWithTotalVM>> SearchPlaces(SearchPlacesIM spim);
    Task<Places> GetPlaceById(int placeId);
    Task<bool> InsertPlace(InsertPlaceWithoutUserIdIM place, Guid Id);
    Task<bool> UpdatePlace(UpdatePlaceIM place);
    Task<bool> DeletePlace(int placeId);
}


