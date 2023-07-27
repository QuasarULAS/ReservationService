using System;
using Core.Models;
using Infrastructure.Repositories.UserRepo.Model;

namespace Infrastructure.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<bool> InsertUser(InsertUserIM user);
    }
}

