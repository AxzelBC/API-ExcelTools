using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Domain.Request;
using ExcelToolsApi.JWT.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExcelToolsApi.Infraestructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettingDTO _jwtSettingDTO;
    private IDateTimeProvider _dateTimeProvider { get; }
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettingDTO> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettingDTO = jwtOptions.Value;
    }

    public string GenerateToken(TokenRequest tokenRequest)
    {
        var siginingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingDTO.Secret)),
            SecurityAlgorithms.HmacSha256
        );
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, tokenRequest.UserId.ToString()),
            new Claim("user_id", tokenRequest.UserId.ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettingDTO.Issuer,
            audience: _jwtSettingDTO.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettingDTO.ExpiryMinutes),
            claims: claims,
            signingCredentials: siginingCredentials
         );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
