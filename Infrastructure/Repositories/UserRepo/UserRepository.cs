using System.Data;
using System.Data.SqlClient;
using Dapper;
using Infrastructure.Repositories.UserRepo.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.UserRepo;

public class UserRepository
{
    private static string _connectionString;

    public UserRepository(IConfiguration iconfiguration)
    {
        _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
    }

    public static List<UsersViewModel> GetAllUsers()
    {
        List<UsersViewModel> list;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            list = db.Query<UsersViewModel>("select * from Users").ToList();
        }
        return list;
    }

    public static bool InsertUser(InsertUserInputModel user)
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "insert into Users(Username,Password,Status) values(@Username,@Password,@Status)";
                db.Execute(query, user);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}