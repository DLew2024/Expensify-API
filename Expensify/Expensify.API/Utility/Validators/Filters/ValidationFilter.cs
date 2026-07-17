using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Expensify.API.Utility.Validators.Filters;

public class ValidationFilter<TRequest>(IValidator<TRequest> validator) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments.Values.OfType<TRequest>().FirstOrDefault();

        if (request is null)
        {
            await next();
            return;
        }

        var validationResult = await validator.ValidateAsync(
            request,
            context.HttpContext.RequestAborted
        );

        if (!validationResult.IsValid)
        {
            context.Result = new BadRequestObjectResult(
                validationResult.Errors.Select(error => new
                {
                    error.PropertyName,
                    error.ErrorMessage,
                    error.ErrorCode,
                })
            );

            return;
        }

        await next();
    }
}
