using HttpLogger.Loggers.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace HttpLogger.Loggers
{
    public class TextFileLogger : ICustomLogger
    {
        public void Log(HttpRequest request, JwtSecurityToken jwtToken)
        {
            using var stream = new FileStream("requestlog.txt", FileMode.Append);
            using var writer = new StreamWriter(stream);
            writer.WriteLine(string.Empty.PadLeft(50, '-') + @$"
            Method: {request.Method} 
            Path: {request.Path} 
            UserId: {jwtToken.Payload["id"]} 
            UserName: {jwtToken.Payload["username"]} 
            Email: {jwtToken.Payload["email"]} 
            FirstName: {jwtToken.Payload["firstName"]} 
            LastName: {jwtToken.Payload["lastName"]} 
            Gender: {jwtToken.Payload["gender"]} 
            TokenExpDate: {jwtToken.Payload["exp"]}");
        }
    }
}
