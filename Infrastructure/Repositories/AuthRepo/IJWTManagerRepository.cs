using Infrastructure.Repositories.AuthRepo.Model;

namespace Infrastructure.Repositories.AuthRepo;

public interface IJWTManagerRepository
{
    Tokens Authenticate(UserAuthenticateM users);
}