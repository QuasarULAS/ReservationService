using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Enum;

namespace Core.Models;

[Table("Places")]
public class Places
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Column("Title")] public string? Title { get; set; }

    [Column("Address")] public string? Address { get; set; }

    [Column("PlaceTypeId")] public EPlaceType PlaceTypeId { get; set; }

    [Column("GeographicalLocation")] public string? GeographicalLocation { get; set; }

    [Column("RegistrationDate")] public string? RegistrationDate { get; set; }

    [Column("RegistrantID")] public string? RegistrantID { get; set; }
}