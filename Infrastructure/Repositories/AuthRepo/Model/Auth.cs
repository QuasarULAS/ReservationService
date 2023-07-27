using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Repositories.AuthRepo.Model;

public class UserIM
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class UserWithIdDto : UserIM
{
    public Guid ID { get; set; }
}

[Table("Users")]
public class RegisterUserIM : UserIM
{
    public bool? Status { get; set; }
}

public class Tokens
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}



