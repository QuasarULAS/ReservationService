using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Enum;
using Core.Base.Pagination;

namespace Infrastructure.Repositories.PlaceRepo.Model;

public class InsertPlaceWithoutUserIdIM
{
    public string? Title { get; set; }
    public string? Address { get; set; }
    public EPlaceType? PlaceTypeId { get; set; }
    public string? GeographicalLocation { get; set; }
}

[Table("Places")]
public class InsertPlaceWithUserIdDto : InsertPlaceWithoutUserIdIM
{
    public Guid? RegistrantID { get; set; }
}

public class UpdatePlaceIM
{
    [Required]
    public int ID { get; set; }
    public string? Title { get; set; }
    public string? Address { get; set; }
    public EPlaceType? PlaceTypeId { get; set; }
    public string? GeographicalLocation { get; set; }
    public Guid? RegistrantID { get; set; }
}

public class SearchPlacesWithTotalVM
{
    public int Total { get; set; }
    public int PlaceID { get; set; }
    public string? PlaceName { get; set; }
    public string? Address { get; set; }
    public string? GeographicalLocation { get; set; }
    public string? PlaceKind { get; set; }
    public string? BookingPerson { get; set; }
}

public class SearchPlacesIM : BasePaginationVM
{
    public string? PlaceName { get; set; }
    public string? PlaceKind { get; set; }
}