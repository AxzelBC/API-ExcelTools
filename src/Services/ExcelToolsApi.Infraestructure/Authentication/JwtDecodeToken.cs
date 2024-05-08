using System.IdentityModel.Tokens.Jwt;
using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.JWT.Service;

namespace ExcelToolsApi.Infraestructure.Authentication;

public class JwtDecodeToken : IJwtDecodeToken
{
    public JwtDecodeTokenResponseDTO DecodeToken(string Token)
    {

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(Token);

        var id = jwtSecurityToken.Claims.First(claim => claim.Type == "sub").Value;
        var firstName = jwtSecurityToken.Claims.First(claim => claim.Type == "given_name").Value;

        var response = new JwtDecodeTokenResponseDTO { Id = new Guid(id), FirstName = firstName };
        return response;
    }
}
