using Crypto.Compare.Common.Common.Errors;
using Crypto.Compare.Common.Errors;
using Crypto.Compare.PublicApi.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Compare.PublicApi.Extensions;

/// <summary>
///     Extensions for api result
/// </summary>
public static class ApiResponseExtensions
{
    public static IActionResult ToApiResultAsync<TEntity, TResponse>(
        this Result<TEntity> cqrsRequest,
        Func<Result<TEntity>?, TResponse> responseMapper)
        where TResponse : BaseApiResponse
    {
        return cqrsRequest.IsSuccess ? responseMapper(cqrsRequest).ToObjectResult() : cqrsRequest.ToApiErrorResult();
    }

    public static IActionResult ToApiResult<TEntity>(this Result<TEntity> result)
    {
        return result.IsSuccess ? new BaseApiResponse().ToObjectResult() : result.ToApiErrorResult();
    }

    public static IActionResult ToApiResult(this Result result)
    {
        return result.IsSuccess ? new BaseApiResponse().ToObjectResult() : result.ToApiErrorResult();
    }

    public static IActionResult ToApiErrorResult(this IResultBase result)
    {
        return new BaseApiResponse().FillErrors(result).ToObjectResult();
    }

    public static IActionResult ToApiErrorResult(this IResultBase result, int statusCode)
    {
        return new BaseApiResponse().FillErrors(result).ToObjectResult(statusCode);
    }

    public static BaseApiResponse ToApiError(this IResultBase result)
    {
        return new BaseApiResponse().FillErrors(result);
    }

    private static BaseApiResponse FillErrors(this BaseApiResponse response, IResultBase result)
    {
        foreach (var error in result.Errors)
        {
            if (error is ApplicationError applicationError)
                response.AddErrorMsg(applicationError.ErrorCode, error.Message);
            else
                response.AddErrorMsg((int)SystemErrorCodes.InvalidRequest, error.Message);
        }

        return response;
    }

    private static IActionResult ToObjectResult<TApiResponse>(this TApiResponse response)
        where TApiResponse : BaseApiResponse
    {
        if (response.HasError)
        {
            return response.ErrorCode switch
            {
                (int)SystemErrorCodes.NotFound => new NotFoundObjectResult(response),
                (int)SystemErrorCodes.SystemError => new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                _ => new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                }
            };
        }

        return new OkObjectResult(response);
    }
}