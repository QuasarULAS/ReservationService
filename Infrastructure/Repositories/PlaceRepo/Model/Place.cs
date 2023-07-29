using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Enum;
using Core.Base.Pagination;

namespace Infrastructure.Repositories.PlaceRepo.Model;

public class InsertPlaceWithoutUserIdIM
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public EPlaceType PlaceTypeId { get; set; }
    [Required]
    public string GeographicalLocation { get; set; }
}

[Table("Places")]
public class InsertPlaceWithUserIdDto : InsertPlaceWithoutUserIdIM
{
    [Required]
    public Guid RegistrantID { get; set; }
}

[Table("Places")]
public class UpdatePlaceIM
{
    [Key]
    [Required]
    public int ID { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public EPlaceType PlaceTypeId { get; set; }
    [Required]
    public string GeographicalLocation { get; set; }
    [Required]
    public Guid RegistrantID { get; set; }
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