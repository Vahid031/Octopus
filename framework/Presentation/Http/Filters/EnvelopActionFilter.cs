using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Presentation.Http.Filters;

public class EnvelopActionFilter : IAlwaysRunResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ObjectResult objectResult)
            return;

        if (objectResult.StatusCode is >= 400 and < 500)
        {
            if (objectResult.Value is EnvelopError envelopError)
            {
                context.Result = new ObjectResult(new FailureEnvelop(envelopError))
                {
                    StatusCode = objectResult.StatusCode
                };

                return;
            }
            if (objectResult.Value is FailureEnvelop failureEnvelop)
            {
                context.Result = new ObjectResult(failureEnvelop)
                {
                    StatusCode = objectResult.StatusCode
                };

                return;
            }

            var envelopErrors = objectResult.Value as IEnumerable<EnvelopError> ?? new[]
            {
                EnvelopError.Create("invalid.input", "Request is not valid")
            };

            context.Result = new ObjectResult(new FailureEnvelop(envelopErrors))
            {
                StatusCode = objectResult.StatusCode
            };

            return;
        }

        context.Result = new ObjectResult(new SuccessEnvelop(objectResult.Value))
        {
            StatusCode = objectResult.StatusCode
        };
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {

    }
}
