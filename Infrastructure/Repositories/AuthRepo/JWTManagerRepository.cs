using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Infrastructure.Repositories.AuthRepo.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.AuthRepo;

public class JWTManagerRepository : IJWTManagerRepository
{
    private readonly IConfiguration iconfiguration;
    private static string? _connectionString;

    public JWTManagerRepository(IConfiguration iconfiguration)
    {
        _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
    }

    public Tokens Authenticate(UserAuthenticateM users)
    {
        IDbConnection db = new SqlConnection(_connectionString);
        db.Open();
        var UsersRecords =
            db.Query<UserWithId>(
                "select Username,Password,ID from Users where Username=@Username and Password=@Password",
                new { users.Username, users.Password }).FirstOrDefault();
        db.Close();

        if (UsersRecords == null) return null;

        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new(ClaimTypes.Name, UsersRecords.Username),
                new Claim("ID", UsersRecords.ID.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(100),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Tokens { Token = tokenHandler.WriteToken(token) };
    }
}