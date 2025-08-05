using FluentResults;

namespace Crypto.Compare.Services.Extensions;

public static class ResultExtensions
{
    public static string GetErrorString<TEntity>(this Result<TEntity> request)
    {
        return request.Errors.GetErrorFormatted();
    }

    public static string GetErrorString(this Result request)
    {
        return request.Errors.GetErrorFormatted();
    }

}