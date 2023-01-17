using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HttpLogger.Loggers;
using HttpLogger.Loggers.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpLogger.Middlewares
{
    public class RequestLoggingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ICustomLogger _customLoggercustomLogger;

        public RequestLoggingMiddleware(RequestDelegate next, ICustomLogger customLoggercustomLogger)
        {
            _next = next;
            _customLoggercustomLogger = customLoggercustomLogger;
        }

        public async Task Invoke(HttpContext context)
        {

            await _next(context);

            var request = context.Request;

            var authHeader = request.Headers["Authorization"].ToString();
            var token = string.Empty;
            if (string.IsNullOrEmpty(authHeader)) return;

            token = authHeader.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            _customLoggercustomLogger.Log(request, jwtToken);
        }
    }
}