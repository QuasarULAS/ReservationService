using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Enum;
using Core.Base.Pagination;

namespace Infrastructure.Repositories.PlaceRepo.Model;

public class InsertPlaceWithoutUserIdIM
{
    [Required(ErrorMessage = "تایتل مکان الزامیست.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "آدررس مکان الزامیست.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "نوع مکان الزامیست.")]
    public EPlaceType PlaceTypeId { get; set; }
    [Required(ErrorMessage = "مکان جغرافیایی مکان الزامیست.")]
    public string GeographicalLocation { get; set; }
}

[Table("Places")]
public class InsertPlaceWithUserIdDto : InsertPlaceWithoutUserIdIM
{
    [Required(ErrorMessage = "آیدی کاربر ثبت کننده مکان الزامیست.")]
    public Guid RegistrantID { get; set; }
}

[Table("Places")]
public class UpdatePlaceIM : InsertPlaceWithUserIdDto
{
    [Key]
    [Required(ErrorMessage = "آیدی مکان الزامیست.")]
    public int ID { get; set; }
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