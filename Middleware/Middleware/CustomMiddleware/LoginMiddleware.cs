using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Middleware.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path == "/" && httpContext.Request.Method == "POST")
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

                string? email = null, password = null;

                if(queryDict.ContainsKey("email"))
                {
                    email = Convert.ToString(queryDict["email"][0]);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for email\n");
                }

                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for password\n");
                }

                if(string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(password) == false)
                {
                    string validEmail = "admin@example.com", validPassword = "admin1234";
                    bool isValidLogin;

                    if(email == validEmail && password == validPassword)
                    {
                        isValidLogin = true;
                    }
                    else
                    {
                        isValidLogin= false;
                    }

                    if (isValidLogin)
                    {
                        await httpContext.Response.WriteAsync("Successful login\n");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid login\n");
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
