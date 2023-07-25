using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Infrastructure.Repositories.PlaceRepo.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.PlaceRepo;

public class PlaceRepository
{
    private static string _connectionString;

    public PlaceRepository(IConfiguration iconfiguration)
    {
        _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
    }

    public static object GetAllPlaces(SearchPlacesDto spim)
    {
        if (spim == null) return new { err = "null" };
        if (spim.Page == null || spim.Limit == null) return new { err = "null pl" };
        if (spim.Page <= 0 || spim.Limit <= 0) return new { err = "<0" };

        int totalCount;
        List<SearchPlacesWithoutTotalDto> _list;
        List<SearchPlacesWithTotalDto> list;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            list = db.Query<SearchPlacesWithTotalDto>("MYSP_Search_Places_By_Title_And_Kind", spim,
                commandType: CommandType.StoredProcedure).ToList();
        }

        if (list.Count == 0) return new { err = "none" };

        totalCount = list[0].TotalRecords;
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
        allPlaces.TotalRecords = totalCount;
        allPlaces.data = _list;
        return allPlaces;
    }

    public static bool InsertPlace(InsertPlaceWithoutUserIdDto place, string Id)
    {
        try
        {
            var newPlace = new InsertPlaceWithUserIdDto();
            newPlace.Title = place.Title;
            newPlace.Address = place.Address;
            newPlace.GeographicalLocation = place.GeographicalLocation;
            newPlace.PlaceTypeId = place.PlaceTypeId;
            newPlace.RegistrantID = Id;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query =
                    "INSERT INTO Places(Title,Address,PlaceTypeId,GeographicalLocation,RegistrantID) VALUES" +
                    "(@Title,@Address,@PlaceTypeId,@GeographicalLocation,@RegistrantID)";
                db.Execute(query, newPlace);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static int EditPlace(UpdatePlaceDto place)
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var underUpdatePlace =
                    db.Query<PlacesVm>("select * from places where ID = @ID", place).FirstOrDefault();
                if (underUpdatePlace == null) return 2;

                var sb = new StringBuilder("UPDATE Places SET Title=COALESCE(@Title,Title)");
                if (place.Address != null) sb.Append(",Address=@Address");
                if (place.PlaceTypeId != null) sb.Append(",PlaceTypeId=@PlaceTypeId");
                if (place.GeographicalLocation != null) sb.Append(",GeographicalLocation=@GeographicalLocation");
                if (place.RegistrantID != null) sb.Append(",RegistrantID=@RegistrantID");
                sb.Append(" WHERE ID = @ID ");
                var query = sb.ToString();

                db.Execute(query, place);
            }

            return 0;
        }
        catch
        {
            return 1;
        }
    }

    public static int DeletePlace(int placeId)
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var underDeletePlace =
                    db.Query<PlacesVm>("select * from places where ID = " + placeId).FirstOrDefault();
                if (underDeletePlace == null) return 2;
                var query = "DELETE FROM Places WHERE ID=@ID";
                db.Execute(query, new { ID = placeId });
            }

            return 0;
        }
        catch (Exception)
        {
            return 1;
        }
    }
}