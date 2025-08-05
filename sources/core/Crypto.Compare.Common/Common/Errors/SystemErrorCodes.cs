namespace Crypto.Compare.Common.Common.Errors;

/// <summary>
/// System errors
/// </summary>
public enum SystemErrorCodes
{
    None = 0,

    /// <summary>
    /// System error, request was shurted down
    /// </summary>
    SystemError = -1,

    /// <summary>Invalid http rquest</summary>
    InvalidRequest = -2,

    /// <summary>
    /// The request is empty
    /// </summary>
    EmptyRequest = -3,

    /// <summary>
    /// Use is not authoorizate
    /// </summary>
    Unathorized = -401,

    /// <summary>
    /// Access denied
    /// </summary>
    Forbidden = -403,

    /// <summary>
    /// Method or page not found
    /// </summary>
    NotFound = -404,

    /// <summary>
    /// Service not available
    /// </summary>
    TemporaryUnavailable = -4,

    /// <summary>
    /// Unhandled exception
    /// </summary>
    UnhandledException = -5,

    /// <summary>
    /// All request was calnceled
    /// </summary>
    RequestCancel = -6,

    /// <summary>
    /// Service is disabled
    /// </summary>
    ServiceIsDisabled = -7,
}
