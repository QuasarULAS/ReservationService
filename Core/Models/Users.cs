using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

[Table("Users")]
public class Users
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }

    [Column("Username")] public string? Username { get; set; }

    [Column("Password")] public string? Password { get; set; }

    [Column("Status")] public bool Status { get; set; }

    [Column("RegisterDate")] public DateTime RegisterDate { get; set; }
}