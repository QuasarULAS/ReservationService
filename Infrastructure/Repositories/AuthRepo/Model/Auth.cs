using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Repositories.AuthRepo.Model;

public class UserIM
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}

public class UserWithIdDto : UserIM
{
    public Guid ID { get; set; }
}

[Table("Users")]
public class RegisterUserIM : UserIM
{
    [Required]
    public bool Status { get; set; }
}

public class Tokens
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}



