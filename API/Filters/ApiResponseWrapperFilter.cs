using API.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ApiResponseWrapperFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(
        ResultExecutingContext context, ResultExecutionDelegate next)
    {
        switch (context.Result)
        {
            case ObjectResult { Value: not null } obj
                when IsSuccess(obj.StatusCode) && !IsAlreadyWrapped(obj.Value):
                obj.Value = ApiResponse<object>.Ok(obj.Value);
                obj.DeclaredType = typeof(ApiResponse<object>);
                break;


            case OkResult:
                context.Result = new OkObjectResult(ApiResponse<object>.Ok(null));
                break;
        }

        await next();
    }

    private static bool IsSuccess(int? code) => code is null or (>= 200 and < 300);

    private static bool IsAlreadyWrapped(object value) =>
        value.GetType() is { IsGenericType: true } t &&
        t.GetGenericTypeDefinition() == typeof(ApiResponse<>);
}