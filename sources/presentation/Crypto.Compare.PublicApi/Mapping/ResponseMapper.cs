using AutoMapper;
using Crypto.Compare.PublicApi.Extensions;
using Crypto.Compare.PublicApi.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Compare.PublicApi.Mapping;

public class ResponseMapper : IResponseMapper
{
    private readonly IMapper _automapper;

    public ResponseMapper(IMapper automapper)
    {
        _automapper = automapper;
    }

    public IActionResult ToCustomResponse<TResponse, TEntity>(Result<TEntity>? result)
        where TResponse : BaseApiResponse, new()
    {
        if (result == null) return null;

        if (result.IsSuccess) return _automapper.Map<TResponse>(result.Value).ToObjectResult();

        return result.ToApiErrorResult();
    }
}