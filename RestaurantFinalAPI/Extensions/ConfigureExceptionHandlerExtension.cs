using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Serilog;

namespace RestaurantFinalAPI.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    //http context xeta kodun veririk 500>
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //content type                  mime fw daha guvenli 
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    

                        //bu fature islemir guya try-catchdan goturur seyi
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                           
                            //Log.Error($"Source: {contextFeature.Error.Source} - Message: {contextFeature.Error.Message}");
                            logger.LogError($"Source: {contextFeature.Error.Source} - Message: {contextFeature.Error.Message}");

                            //Http respons donuruk geriye burda json formatinda
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                                Title = "Internal Error RestaurantApi"
                            })); ;
                        }
                });
            });
        }
    }
}
