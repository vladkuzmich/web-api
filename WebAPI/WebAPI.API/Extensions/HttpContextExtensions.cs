using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAPI.API.Models.Documents;

namespace WebAPI.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static Task ConfigureErrorResponseAsync(this HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDocument
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal server error",
            }.ToString());
        }
    }
}
