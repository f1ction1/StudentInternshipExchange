using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace InternshipExchange.Api.ModelBinders.CommaSeparated
{
    public class CommaSeparatedModelBinder : IModelBinder
    {
        private readonly Type _elementType;

        public CommaSeparatedModelBinder(Type elementType)
        {
            _elementType = elementType ?? throw new ArgumentNullException(nameof(elementType));
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var values = valueProviderResult.Values;
            var raw = values.ToArray();

            // Combine repeated params and comma-separated values
            var tokens = raw
                .SelectMany(r => r?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>())
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            if (tokens.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var targetListType = typeof(List<>).MakeGenericType(_elementType);
            var list = (IList)Activator.CreateInstance(targetListType)!;

            var underlyingType = Nullable.GetUnderlyingType(_elementType) ?? _elementType;

            foreach (var token in tokens)
            {
                try
                {
                    object? converted;
                    if (underlyingType == typeof(Guid))
                    {
                        converted = Guid.Parse(token);
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(underlyingType);
                        converted = converter.ConvertFromString(null, CultureInfo.InvariantCulture, token);
                    }

                    list.Add(converted);
                }
                catch
                {
                    // could be logged or added ModelState error
                }
            }

            bindingContext.Result = ModelBindingResult.Success(list);
            return Task.CompletedTask;
        }
    }
}
