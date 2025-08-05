using Crypto.Compare.PublicApi.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Compare.PublicApi.Mapping;

public interface IResponseMapper
{
    /// <summary>
    /// Mapper from results to response
    /// </summary>
    /// <returns></returns>
    IActionResult ToCustomResponse<TResponse, TEntity>(Result<TEntity> result)
        where TResponse : BaseApiResponse, new();

}
