using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extentions
{
    public class SwaggerHeader:IOperationFilter
    {
        
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();


            var cultureOptions = new List<IOpenApiAny>
        {
            new OpenApiString("en"),
            new OpenApiString("ar")
        };

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "culture",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = cultureOptions, // ✅ هنا نحدد القيم كـ Dropdown
                    Default = new OpenApiString("en")
                },
                Description = "Specify culture (e.g. ar or en)"
            });
        }
    }
}
