using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Repositories.AuthRepo.Model;

public class UserIM
{
    [Required(ErrorMessage = "نام کاربری الزامیست.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "پسورد الزامیست.")]
    public string Password { get; set; }
}

public class UserWithIdDto : UserIM
{
    public Guid ID { get; set; }
}

[Table("Users")]
public class RegisterUserIM : UserIM
{
    [Required(ErrorMessage = "وضیعیت فعال یا غیرفعال بودن الزامیست.")]
    public bool Status { get; set; }
}

public class Tokens
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}



