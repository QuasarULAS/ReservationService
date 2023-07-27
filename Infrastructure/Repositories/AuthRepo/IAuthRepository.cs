using Infrastructure.Repositories.AuthRepo.Model;

namespace Infrastructure.Repositories.AuthRepo;

public interface IAuthRepository
{
    Task<UserWithIdDto> Authenticate(UserIM users);
    Task<bool> RegisterUser(RegisterUserIM user);
}