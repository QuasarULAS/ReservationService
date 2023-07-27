using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Repositories.PlaceRepo.Model;

public class BasePlace
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public int PlaceTypeId { get; set; }
    public string GeographicalLocation { get; set; }
}

public class PlacesVm : BasePlace
{
    public DateTime RegistrationDate { get; set; }
    public Guid RegistrantID { get; set; }
}

public class InsertPlaceWithoutUserIdDto
{
    public string Title { get; set; }
    public string Address { get; set; }
    public int PlaceTypeId { get; set; }
    public string GeographicalLocation { get; set; }
}

[Table("Places")]
public class InsertPlaceWithUserIdDto : InsertPlaceWithoutUserIdDto
{
    public string RegistrantID { get; set; }
}

public class UpdatePlaceDto
{
    public int? ID { get; set; }
    public string? Title { get; set; }
    public string? Address { get; set; }
    public int? PlaceTypeId { get; set; }
    public string? GeographicalLocation { get; set; }
    public Guid? RegistrantID { get; set; }
}

public class SearchPlacesWithTotalDto
{
    public int Total { get; set; }
    public int PlaceID { get; set; }
    public string PlaceName { get; set; }
    public string Address { get; set; }
    public string GeographicalLocation { get; set; }
    public string PlaceKind { get; set; }
    public string BookingPerson { get; set; }
}

public class SearchPlacesWithTotalAndListDto
{
    public int Total { get; set; }
    public IEnumerable<SearchPlacesWithoutTotalDto> data { get; set; }
}

public class SearchPlacesWithoutTotalDto
{
    public int PlaceID { get; set; }
    public string PlaceName { get; set; }
    public string Address { get; set; }
    public string GeographicalLocation { get; set; }
    public string PlaceKind { get; set; }
    public string BookingPerson { get; set; }
}

public class SearchPlacesDto
{
    public string? PlaceName { get; set; }
    public string? PlaceKind { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
}