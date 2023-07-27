using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Repositories.UserRepo.Model;

[Table("Users")]
public class InsertUserIM
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool? Status { get; set; }
}