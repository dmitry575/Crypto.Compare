using Crypto.Compare.Common.Common.Errors;
using Crypto.Compare.PublicApi.Extensions;
using Crypto.Compare.PublicApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crypto.Compare.PublicApi.Filters;

/// <summary>
///     Filter of validation response
/// </summary>
public class ApiRequestValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionArguments = context.ActionArguments;
        if (actionArguments.Count == 1)
        {
            var request = actionArguments.First();

            if (request.Value == null)
            {
                var message = "Empty request";
                context.ModelState.AddModelError(string.Empty, message);
            }
        }

        if (context.ModelState.IsValid) return;

        var response = CreateBaseApiResponse(context);
        context.Result = response.ToObjectResult();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    ///     Create response with error
    /// </summary>
    private BaseApiResponse CreateBaseApiResponse(ActionContext actionContext)
    {
        var response = new BaseApiResponse
        {
            ErrorCode = (int)SystemErrorCodes.InvalidRequest
        };

        foreach (var modelError in actionContext.ModelState.Values.SelectMany(e => e.Errors))
        {
            var msg = modelError.ErrorMessage;
            if (string.IsNullOrWhiteSpace(msg)) msg = "Invalid request.";

            response.AddErrorMsg(response.ErrorCode, msg);
        }

        return response;
    }
}