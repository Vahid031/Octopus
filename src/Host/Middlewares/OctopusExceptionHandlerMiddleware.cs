using Octopus.Core.Domain.Exceptions;
using System.Diagnostics;
using System.Net;

namespace Octopus.Host.Middlewares;

public class OctopusExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<OctopusExceptionHandlerMiddleware> _logger;

    public OctopusExceptionHandlerMiddleware(ILogger<OctopusExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        //catch (SquidwardNotAuthorizedException exp)
        //{
        //    LogDistributedTracing(exp);
        //    await WriteResponse(exp, context, "unauthorized", HttpStatusCode.Unauthorized, message: exp.Message);
        //}
        //catch (SquidwardEntityNotFoundException exp)
        //{
        //    LogDistributedTracing(exp);
        //    await WriteResponse(exp, context, "entity.not.found", HttpStatusCode.NotFound, message: exp.Message);
        //}
        catch (OctopusDomainException exp)
        {
            LogDistributedTracing(exp);
            await WriteResponse(exp, context, "", HttpStatusCode.BadRequest, message: exp.Message);
        }
        catch (Exception exp)
        {
            LogDistributedTracing(exp);
            await WriteResponseAndLogError(exp, context, "internal.error", HttpStatusCode.InternalServerError);
        }
    }

    private void LogDistributedTracing(Exception exp)
    {

        if (Activity.Current != null)
        {
            Activity.Current.AddTag("exception.type", exp.GetType());
            Activity.Current.AddTag("exception.message", exp.Message);
            if (exp.InnerException != null)
            {
                Activity.Current.AddTag("exception.inner.type", exp.InnerException.GetType());
                Activity.Current.AddTag("exception.inner.message", exp.InnerException.Message);
            }
        }
    }

    private async Task WriteResponseAndLogError(Exception exception, HttpContext context, string errorKey, HttpStatusCode statusCode, string message = null)
    {
        try
        {
            await WriteResponse(exception, context, errorKey, statusCode, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception handling failed!");
        }
        _logger.LogError(exception, "Key: {ErrorKey}. Msg: {Message}", errorKey, message);
    }

    private async Task WriteResponse(Exception exception, HttpContext context, string errorKey, HttpStatusCode statusCode, string message = null)
    {
        try
        {
            context.Response.StatusCode = (int)statusCode;

            //var decodedQueryString = WebUtility.UrlDecode($"{context.Request.QueryString}".Truncate(512));
            //exception.Data.TryAdd("request_path", $"{context.Request.Path}");
            //exception.Data.TryAdd("request_query_string", decodedQueryString);

            //var response = new FailureEnvelop(new[]
            //{
            //    EnvelopError.Create(code: errorKey, message: message ?? "Oops, something went wrong")
            //});
            //await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception handling failed!");
        }
    }
}
