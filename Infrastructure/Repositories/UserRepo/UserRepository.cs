using System.Data;
using Core.Models;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Repositories.UserRepo.Model;

namespace Infrastructure.Repositories.UserRepo;

public class UserRepository
{
    private static DbSession _session;

    public UserRepository(DbSession session)
    {
        _session = session;
    }

    public async Task<List<Users>> GetAllUsers()
    {
        List<Users> list;
        list = (await _session.Connection.QueryAsync<Users>("select * from Users")).ToList();
        return list;
    }

    public async Task<bool> InsertUser(InsertUserIM user)
    {
        await _session.Connection.InsertAsync(user);
        return true;
    }
}