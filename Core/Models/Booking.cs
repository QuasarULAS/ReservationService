using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

[Table("BookLog")]
public class BookLog
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Column("RegistrationDate")] public DateTime RegistrationDate { get; set; }

    [Column("ReservationDate")] public DateTime ReservationDate { get; set; }

    [Column("BookingPersonId")] public Guid? BookingPersonId { get; set; }

    [Column("BookingPlaceId")] public int? BookingPlaceId { get; set; }

    [Column("Price")] public int Price { get; set; }
}