using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InternshipExchange.Api.ModelBinders.CommaSeparated
{
    public class CommaSeparatedModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            var modelType = context.Metadata.ModelType;
            if (modelType == typeof(string)) return null;

            Type? elementType = null;

            if (modelType.IsArray)
            {
                elementType = modelType.GetElementType();
            }
            else if (modelType.IsGenericType)
            {
                var genDef = modelType.GetGenericTypeDefinition();
                if (genDef == typeof(IEnumerable<>) || genDef == typeof(IList<>) || genDef == typeof(List<>))
                {
                    elementType = modelType.GetGenericArguments()[0];
                }
            }

            if (elementType == null) return null;

            if (!IsSimpleType(elementType)) return null;

            return new CommaSeparatedModelBinder(elementType);
        }

        private bool IsSimpleType(Type t)
        {
            var u = Nullable.GetUnderlyingType(t) ?? t;
            return u.IsPrimitive
                   || u == typeof(string)
                   || u == typeof(decimal)
                   || u == typeof(DateTime)
                   || u == typeof(Guid);
        }
    }
}
