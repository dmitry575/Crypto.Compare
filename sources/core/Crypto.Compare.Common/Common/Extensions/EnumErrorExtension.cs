using Crypto.Compare.Common.Errors;

namespace Crypto.Compare.Common.Common.Extensions;

public static class EnumErrorExtension
{
    /// <summary>
    /// Convert to error <see cref="ApplicationError"/>
    /// </summary>
    public static ApplicationError AsError(this Enum errorCode, string message) => new(errorCode, message);
}
