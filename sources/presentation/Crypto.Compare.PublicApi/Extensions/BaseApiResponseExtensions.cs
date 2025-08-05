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
            switch (response.ErrorCode)
            {
                case (int)SystemErrorCodes.NotFound:
                    return new NotFoundObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }

        if (statusCodes != null)
        {
            switch (statusCodes.Value)
            {
                case 400:
                    return new UnauthorizedObjectResult(response);
            }
        }

        return new OkObjectResult(response);
    }
}
