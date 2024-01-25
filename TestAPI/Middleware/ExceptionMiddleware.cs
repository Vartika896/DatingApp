using System.Net;
using System.Text.Json;
using TestAPI.Errors;

namespace TestAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ApiException> logger;
        private readonly IHostEnvironment host;
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ApiException> logger, 
        IHostEnvironment host)
        {
            this.next = next;
            this.host = host;
            this.logger = logger;
    
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType= "application/json";
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;

                var response= host.IsDevelopment()?
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");
                
                var option= new JsonSerializerOptions{PropertyNamingPolicy= JsonNamingPolicy.CamelCase};

                var json= JsonSerializer.Serialize(response,option);

                await context.Response.WriteAsync(json);

            }
        }
    }
}