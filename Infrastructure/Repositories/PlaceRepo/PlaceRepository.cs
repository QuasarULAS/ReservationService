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

    public async Task<List<SearchPlacesWithTotalVM>> SearchPlaces(SearchPlacesIM spim)
    {
        List<SearchPlacesWithTotalVM> list;
        list = (await _session.Connection.QueryAsync<SearchPlacesWithTotalVM>("MYSP_Search_Places_By_Title_And_Kind", spim,
            commandType: CommandType.StoredProcedure)).ToList();
        return list;
    }

    public async Task<Places> GetPlaceById(int placeId)
    {
        Places? Place;
        Place = await _session.Connection.GetAsync(new Places { ID = placeId });
        return Place;
    }

    public async Task<bool> InsertPlace(InsertPlaceWithoutUserIdIM place, Guid Id)
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

    public async Task<bool> UpdatePlace(UpdatePlaceIM place)
    {
        bool IsUpdate;
        IsUpdate = await _session.Connection.UpdateAsync(new UpdatePlaceIM
        {
            ID = (int)place.ID,
            Title = place.Title,
            Address = place.Address,
            PlaceTypeId = place.PlaceTypeId,
            GeographicalLocation = place.GeographicalLocation,
            RegistrantID = place.RegistrantID,
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