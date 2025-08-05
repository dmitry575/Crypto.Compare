using System.Globalization;
using FluentResults;

namespace Crypto.Compare.Common.Errors;

/// <summary>
/// Error with error code
/// </summary>
public class ApplicationError: Error
{
    public ApplicationError(Enum errorCode, string errorMessage)
        : base(errorMessage)
    {
        ErrorCode = Convert.ToInt32(errorCode, CultureInfo.InvariantCulture);
    }

    public int ErrorCode { get; set; }

    public static Result Create(Enum errorCode, string errorMessage)
    {
        return Result.Fail(new ApplicationError(errorCode, errorMessage));
    }
}
