namespace Infrastructure.Repositories.AuthRepo.Model;

public class UserWithId
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}