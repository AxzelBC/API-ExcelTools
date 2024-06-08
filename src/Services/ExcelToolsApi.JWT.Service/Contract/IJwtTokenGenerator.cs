using ExcelToolsApi.Domain.Request;

namespace ExcelToolsApi.JWT.Service;

public interface IJwtTokenGenerator
{
    string GenerateToken(TokenRequest tokenRequest);
}