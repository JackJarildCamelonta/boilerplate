using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Boilerplate.WebApi.Configuration.Swagger
{
    /// <summary>
    /// AutoRest schema filter
    /// </summary>
    public class AutoRestSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply the filter
        /// </summary>
        /// <param name="schema">Schema model</param>
        /// <param name="context">Schema context</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type.IsEnum)
            {
                schema.Extensions.Add(
                    "x-ms-enum",
                    new OpenApiObject
                    {
                        ["name"] = new OpenApiString(type.Name),
                        ["modelAsString"] = new OpenApiBoolean(false)
                    }
                );
            };
        }
    }
}
