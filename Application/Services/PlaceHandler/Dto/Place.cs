namespace Application.Services.PlaceHandler.Dto;

public class SearchPlacesWithTotalAndListVM
{
    public int Total { get; set; }
    public IEnumerable<SearchPlacesWithoutTotalDto>? data { get; set; }
}

public class SearchPlacesWithoutTotalDto
{
    public int PlaceID { get; set; }
    public string? PlaceName { get; set; }
    public string? Address { get; set; }
    public string? GeographicalLocation { get; set; }
    public string? PlaceKind { get; set; }
    public string? BookingPerson { get; set; }
}