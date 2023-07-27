using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Repositories.AuthRepo.Model;

namespace Infrastructure.Repositories.AuthRepo;

public class AuthRepository : IAuthRepository
{
    private static DbSession _session;

    public AuthRepository(DbSession session)
    {
        _session = session;
    }

    public async Task<UserWithIdDto> Authenticate(UserIM users)
    {
        var UsersRecords = (await _session.Connection.QueryAsync<UserWithIdDto>("select Username,Password,ID from Users where Username=@Username and Password=@Password", new { users.Username, users.Password })).FirstOrDefault();
        return UsersRecords;
    }

    public async Task<bool> RegisterUser(RegisterUserIM user)
    {
        await _session.Connection.InsertAsync(user);
        return true;
    }
}