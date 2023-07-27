using System;
using Core.Models;
using Infrastructure.Repositories.PlaceRepo.Model;

namespace Infrastructure.Repositories.PlaceRepo
{
    public interface IPlaceRepository
    {
        Task<SearchPlacesWithTotalAndListDto> SearchPlaces(SearchPlacesDto spim);
        Task<Places> GetPlaceById(int placeId);
        Task<bool> InsertPlace(InsertPlaceWithoutUserIdDto place, string Id);
        Task<bool> UpdatePlace(UpdatePlaceDto place);
        Task<bool> DeletePlace(int placeId);
    }
}

