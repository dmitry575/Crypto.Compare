using System.Net.Mail;

namespace Crypto.Compare.Services.Extensions;

/// <summary>
///     Different specific validation
/// </summary>
public static class ValidatorExtensions
{
    /// <summary>
    ///     Validate email by function MailAddress
    /// </summary>
    /// <param name="email">String email</param>
    public static bool IsValidEmail(this string? email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        var trimmedEmail = email.Trim();

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}