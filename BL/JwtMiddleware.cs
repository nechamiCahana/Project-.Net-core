using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var jwtKey = _config["Jwt:Key"];
                var jwtIssuer = _config["Jwt:Issuer"];

                if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer))
                {
                    throw new ArgumentNullException("JWT settings are not configured properly.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                context.Items["User"] = userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JWT Middleware Error: {ex.Message}");
            }
            //private readonly RequestDelegate _next;
            //private readonly IConfiguration _configuration;

            //public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
            //{
            //    _next = next;
            //    _configuration = configuration;
            //}

            //public async Task Invoke(HttpContext context)
            //{
            //    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            //    if (token != null)
            //        AttachUserToContext(context, token);

            //    await _next(context);
            //}

            //private void AttachUserToContext(HttpContext context, string token)
            //{
            //    try
            //    {
            //        var tokenHandler = new JwtSecurityTokenHandler();
            //        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            //        tokenHandler.ValidateToken(token, new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            //            ClockSkew = TimeSpan.Zero
            //        }, out SecurityToken validatedToken);

            //        var jwtToken = (JwtSecurityToken)validatedToken;
            //        var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            //        // attach user to context on successful jwt validation
            //        context.Items["User"] = userId;
            //    }
            //    catch
            //    {
            //        // do nothing if jwt validation fails
            //        // user is not attached to context so request won't have access to secure routes
            //    }
        }
    }
}

