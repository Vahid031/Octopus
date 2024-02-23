using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Presentation.Http.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var modelStateErrors = context.ModelState
            .Where(ms => ms.Value is { Errors: not null } && ms.Value.Errors.Any())
            .Select(ms => new
            {
                ErrorKey = ms.Key,
                ErrorValues = ms.Value!.Errors,
                Value = ms.Value.AttemptedValue
            })
            .ToArray();

        var errors = (
                from modelStateError in modelStateErrors
                let errorValues = modelStateError.ErrorValues
                from errorValue in errorValues
                select Error.Create(
                    $"invalid.{modelStateError.ErrorKey.Replace("$.", "")}",
                    errorValue.ErrorMessage,
                    attemptedValue: modelStateError.Value,
                    property: modelStateError.ErrorKey.Replace("$.", "")))
            .ToArray();

        context.Result = new BadRequestObjectResult(errors);
    }
}