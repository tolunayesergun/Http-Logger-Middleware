using System.IdentityModel.Tokens.Jwt;

namespace HttpLogger.Loggers.Interfaces
{
    public interface ICustomLogger
    {
        void Log(HttpRequest request, JwtSecurityToken jwtToken);
    }
}
