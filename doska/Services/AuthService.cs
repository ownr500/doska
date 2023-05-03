using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using doska.Data.Entities;
using doska.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace doska.Services;

public class AuthService : IAuthService
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly JWTOptions _options;

    public AuthService(IOptions<JWTOptions> options, JwtSecurityTokenHandler jwtSecurityTokenHandler)
    {
        _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        _options = options.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _options.ValidIssuer,
            audience: _options.ValidAudience,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signInCredentials
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return _jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
    }
}