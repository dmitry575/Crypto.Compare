using System.Text.Json.Serialization;

namespace Crypto.Compare.PublicApi.Responses;

/// <summary>
///     Base class of API response
/// </summary>
[Serializable]
public class BaseApiResponse
{
    public BaseApiResponse()
    {
        ErrorMsgs = new List<string>();
    }

    public int ErrorCode { get; set; }

    public IList<string> ErrorMsgs { get; set; }

    [JsonIgnore] public virtual bool HasError => ErrorMsgs.Any() && ErrorCode != 0;

    public void AddErrorMsg(string msg)
    {
        if (ErrorMsgs == null) ErrorMsgs = new List<string>();

        ErrorMsgs.Add(msg);
    }

    public void AddErrorMsg(int code, string msg)
    {
        ErrorCode = code;
        AddErrorMsg(msg);
    }

    public BaseApiResponse WithError(int code, string msg)
    {
        ErrorCode = code;
        AddErrorMsg(msg);
        return this;
    }

    public override string ToString()
    {
        return $"Code: {ErrorCode}, message: {string.Join(",", ErrorMsgs)}";
    }
}