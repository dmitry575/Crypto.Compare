using System.Reflection;
using Crypto.Compare.Common.Errors;
using FluentResults;

namespace Crypto.Compare.Services.Helpers
{
    public static class ErrorResponseHelper
    {
        public static TErrorResponse ErrorResponse<TErrorResponse>(ApplicationError error)
        where TErrorResponse : ResultBase
        {
            if (typeof(TErrorResponse).IsGenericType)
            {
                var genericType = typeof(TErrorResponse).GenericTypeArguments.First();
                var method =
                        typeof(Result).GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x =>
                            x.Name == nameof(Result.Fail)
                            && x.IsGenericMethodDefinition && x.GetParameters().Length == 1
                            && x.GetParameters()[0].ParameterType == typeof(IError));

                if (method is null)
                {
                    throw new InvalidOperationException($"{nameof(Result.Fail)} method not found");
                }

                return (method.MakeGenericMethod(genericType)
                    .Invoke(null, new object[] { error }) as TErrorResponse)!;
            }

            return (Result.Fail(error) as TErrorResponse)!;
        }
    }
}
