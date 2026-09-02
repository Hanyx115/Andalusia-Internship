using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManagerApi.Swagger;

public sealed class AnonymousOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var anonymous = context.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true)
            || context.MethodInfo.DeclaringType?.IsDefined(typeof(AllowAnonymousAttribute), true) == true;
        if (anonymous) operation.Security = [];
    }
}
