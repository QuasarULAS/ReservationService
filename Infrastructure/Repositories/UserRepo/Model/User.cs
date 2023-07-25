namespace Infrastructure.Repositories.UserRepo.Model;

public class UsersViewModel
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
    public DateTime RegisterDate { get; set; }
}

public class InsertUserInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}

public class UpdateUserInputModel
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}