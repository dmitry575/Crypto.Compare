using Crypto.Compare.PublicApi.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Crypto.Compare.PublicApi.Swaggers.Responses;

public class Get400Example : IExamplesProvider<BaseApiResponse>
{
    public BaseApiResponse GetExamples()
    {
        return new BaseApiResponse()
        {
            ErrorCode = -2,
            ErrorMsgs = new List<string> { "Invalid request" }
        };
    }
}