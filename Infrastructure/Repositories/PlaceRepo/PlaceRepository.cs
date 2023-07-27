using System.Data;
using Core.Base.Enum;
using Core.Models;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Repositories.PlaceRepo.Model;

namespace Infrastructure.Repositories.PlaceRepo;

public class PlaceRepository : IPlaceRepository
{
    private static DbSession _session;

    public PlaceRepository(DbSession session)
    {
        _session = session;
    }

    public async Task<SearchPlacesWithTotalAndListDto> SearchPlaces(SearchPlacesDto spim)
    {
        //if (spim == null) return new { err = "null" };
        //if (spim.Page == null || spim.Limit == null) return new { err = "null pl" };
        //if (spim.Page <= 0 || spim.Limit <= 0) return new { err = "<0" };

        int totalCount;
        List<SearchPlacesWithoutTotalDto> _list;
        List<SearchPlacesWithTotalDto> list;
        list = (await _session.Connection.QueryAsync<SearchPlacesWithTotalDto>("MYSP_Search_Places_By_Title_And_Kind", spim,
            commandType: CommandType.StoredProcedure)).ToList();


        //if (list.Count == 0) return new { err = "none" };

        totalCount = list[0].Total;
        _list = list.Select(x => new SearchPlacesWithoutTotalDto
        {
            PlaceID = x.PlaceID,
            PlaceName = x.PlaceName,
            Address = x.Address,
            GeographicalLocation = x.GeographicalLocation,
            BookingPerson = x.BookingPerson,
            PlaceKind = x.PlaceKind
        }).ToList();

        var allPlaces = new SearchPlacesWithTotalAndListDto();
        allPlaces.Total = totalCount;
        allPlaces.data = _list;
        return allPlaces;
    }

    public async Task<Places> GetPlaceById(int placeId)
    {
        Places? Place;
        Place = await _session.Connection.GetAsync(new Places { ID = placeId });
        return Place;
    }

    public async Task<bool> InsertPlace(InsertPlaceWithoutUserIdDto place, string Id)
    {
        var newPlace = new InsertPlaceWithUserIdDto
        {
            Title = place.Title,
            Address = place.Address,
            GeographicalLocation = place.GeographicalLocation,
            PlaceTypeId = place.PlaceTypeId,
            RegistrantID = Id
        };

        await _session.Connection.InsertAsync(newPlace);
        return true;
    }

    public async Task<bool> UpdatePlace(UpdatePlaceDto place)
    {
        bool IsUpdate;
        Places oldData = await GetPlaceById((int)place.ID);
        IsUpdate = await _session.Connection.UpdateAsync(new Places
        {
            ID = (int)place.ID,
            Address = place.Address ?? oldData.Address,
            PlaceTypeId = place.PlaceTypeId.HasValue ? (EPlaceType)place.PlaceTypeId : oldData.PlaceTypeId,
            GeographicalLocation = place.GeographicalLocation ?? oldData.GeographicalLocation,
            RegistrantID = place.RegistrantID ?? oldData.RegistrantID,
        });
        return IsUpdate;
    }

    public async Task<bool> DeletePlace(int placeId)
    {
        bool isDelete;
        isDelete = await _session.Connection.DeleteAsync(new Places { ID = placeId });
        return isDelete;
    }
}