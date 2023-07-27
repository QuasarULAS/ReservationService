using MediatR;
using ResultHelper;
using Infrastructure;
using Infrastructure.Repositories.AuthRepo.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthHandler;

public class AuthenticateRequest : UserIM, IRequest<ApiResult<Tokens>>
{
    public class AuthenticateRequestHandler : IRequestHandler<AuthenticateRequest, ApiResult<Tokens>>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<Tokens> _apiResult;
        public AuthenticateRequestHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IApiResult<Tokens> apiResult)
        {
            _configuration = configuration;
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Tokens>> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {

            UserIM user = new()
            {
                Username = request.Username,
                Password = request.Password
            };
            var User = await _unitOfWork.Auth.Authenticate(user);
            if (User == null)
            {
                return _apiResult.WithError(EStatusCode.UnAuthorize);
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new(ClaimTypes.Name, User.Username),
                new Claim("ID", User.ID.ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _apiResult.WithValue(new Tokens { Token = tokenHandler.WriteToken(token) });
            return _apiResult.WithSuccess(EStatusCode.Success);
        }
    }
}

