using ExcelToolsApi.Domain.DTO;

namespace ExcelToolsApi.JWT.Service;

public interface IJwtDecodeToken
{
    JwtDecodeTokenResponseDTO DecodeToken(string Token);
}
