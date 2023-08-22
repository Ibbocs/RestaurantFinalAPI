using System.Net.Mime;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace RestaurantFinalAPI.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //http context xeta kodun veririk 500>
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //content type                  mime fw daha guvenli 
                context.Response.ContentType = MediaTypeNames.Application.Json;

                _logger.LogError($"-- Status Code: {context.Response.StatusCode} - Source: {ex.Source} - Message: {ex.Message} --"); 
                //_logger.LogError(ex, "An unhandled exception occurred. Midilwareeeeeeeeejfjjrejirjfijfiorejfijrfie");
                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Title = "Internal Error"
                };
                var errorResponseJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorResponseJson);

                //var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                //if (contextFeature != null)
                //{
                //    
                //    Log.Error($"Source: {contextFeature.Error.Source} - Message: {contextFeature.Error.Message}");

                //    //Http respons donuruk geriye burda json formatinda
                //    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                //    {
                //        StatusCode = context.Response.StatusCode,
                //        Message = contextFeature.Error.Message,
                //        Title = "Internal Error RestaurantApi"
                //    })); ;
                //}

            }
        }
    }
}

