namespace Infrastructure.Repositories.BookingRepo.Model;

public class BookLogViewModel
{
    public int ReseveID { get; set; }
    public DateTime PlaceRegisterDate { get; set; }
    public DateTime ReservationDate { get; set; }
    public string BookingPerson { get; set; }
    public string PlaceName { get; set; }
    public string PlaceKind { get; set; }
    public int Price { get; set; }
}

public class MakeBookLogWithoutUserIdDto
{
    public DateTime ReservationDate { get; set; }
    public int BookingPlaceId { get; set; }
    public int Price { get; set; }
}

public class MakeBookLogWithUserIdDto : MakeBookLogWithoutUserIdDto
{
    public string BookingPersonId { get; set; }
}

// this model is for checking if database
// has reservation record or not
public class BookLogRecordsDto
{
    public DateTime ReservationDate { get; set; }
    public int BookingPlaceId { get; set; }
}