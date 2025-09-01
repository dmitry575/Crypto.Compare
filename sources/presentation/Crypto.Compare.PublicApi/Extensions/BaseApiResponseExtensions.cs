using Crypto.Compare.Common.Common.Errors;
using Crypto.Compare.PublicApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Compare.PublicApi.Extensions;

public static class BaseApiResponseExtensions
{
    public static ObjectResult ToObjectResult(this BaseApiResponse response, int? statusCodes = null)
    {
        if (response.HasError)
        {
            return response.ErrorCode switch
            {
                (int)SystemErrorCodes.NotFound => new NotFoundObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }

        if (statusCodes == null)
        {
            return new OkObjectResult(response);
        }

        return statusCodes.Value switch
        {
            400 => new UnauthorizedObjectResult(response),
            _ => new OkObjectResult(response)
        };
    }
}