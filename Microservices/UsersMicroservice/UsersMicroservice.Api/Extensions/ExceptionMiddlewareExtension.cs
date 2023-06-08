using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using UsersMicroservice.Api.Exceptions;

namespace UsersMicroservice.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExeptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is NotFoundException)
                        {
                            NotFoundException exeption = (NotFoundException)contextFeature.Error;
                            context.Response.StatusCode = exeption.StatusCode;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = exeption.StatusCode,
                                Message = exeption.Message
                            }.ToString());

                        }
                        else if (contextFeature.Error is BadRequestException)
                        {
                            BadRequestException exeption = (BadRequestException)contextFeature.Error;
                            context.Response.StatusCode = exeption.StatusCode;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = exeption.StatusCode,
                                Message = exeption.Message
                            }.ToString());
                        }
                        else if (contextFeature.Error is AlreadyExistsException)
                        {
                            AlreadyExistsException exeption = (AlreadyExistsException)contextFeature.Error;
                            context.Response.StatusCode = exeption.StatusCode;
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = exeption.StatusCode,
                                Message = exeption.Message
                            }.ToString());
                        }

                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal server error"
                            }.ToString());
                        }
                    }
                });
            });
        }
    }
}
