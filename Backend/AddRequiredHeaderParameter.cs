using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var addHeaderAttribute = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AddHeaderAttribute>()
            .FirstOrDefault();

        if (addHeaderAttribute != null)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Add a new header parameter to the operation.
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("Bearer ")
                    }
            });
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class AddHeaderAttribute : Attribute
{
}

