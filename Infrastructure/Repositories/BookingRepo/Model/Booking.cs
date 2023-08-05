using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Repositories.BookingRepo.Model;

public class BookLogVM
{
    public int? ReseveID { get; set; }
    public DateTime? PlaceRegisterDate { get; set; }
    public DateTime? ReservationDate { get; set; }
    public string? BookingPerson { get; set; }
    public string? PlaceName { get; set; }
    public string? PlaceKind { get; set; }
    public int? Price { get; set; }
}

public class MakeBookLogWithoutUserIdIM
{
    [Required(ErrorMessage = "زمان رزرو الزامیست.")]
    public DateTime ReservationDate { get; set; }
    [Required(ErrorMessage = "آیدی مکان الزامیست.")]
    public int BookingPlaceId { get; set; }
    [Required(ErrorMessage = "قیمت الزامیست.")]
    [Range(1000, int.MaxValue,ErrorMessage ="قیمت باید بیشتر از ۱۰۰۰ باشد.")]
    public int Price { get; set; }
}
[Table("BookLog")]
public class MakeBookLogWithUserIdDto : MakeBookLogWithoutUserIdIM
{
    [Required]
    public Guid BookingPersonId { get; set; }
}

// this model is for checking if database
// has reservation record or not
public class BookLogRecordsVM
{
    public DateTime ReservationDate { get; set; }
    public int BookingPlaceId { get; set; }
}