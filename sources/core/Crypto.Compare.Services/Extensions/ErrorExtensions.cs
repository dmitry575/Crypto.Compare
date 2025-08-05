using System.Text;
using Crypto.Compare.Common.Errors;
using FluentResults;

namespace Crypto.Compare.Services.Extensions;

public static class ErrorExtensions
{
    public static string GetErrorFormatted(this IList<IError> errors)
    {
        if (errors == null)
        {
            return string.Empty;
        }

        var message = new StringBuilder(128);
        foreach (var error in errors)
        {
            if (message.Length > 0)
            {
                message.Append(", ");
            }

            message.Append(error.GetErrorFormatted());
        }

        return message.ToString();
    }

    public static string GetErrorFormatted(this IError error)
    {
        if (error is ApplicationError e)
        {
            return $"[{e.ErrorCode}] - {e.Message}";
        }

        return error.Message;
    }
}