using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class FiltersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var query = bindingContext.HttpContext.Request.Query;

            // Excluir explicitamente os parâmetros padrão
            var filters = query
                .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order") // Ignora os parâmetros padrão
                .ToDictionary(
                    q => q.Key,
                    q => q.Value.ToString() // Converte valores de `StringValues` para `string`
                );

            bindingContext.Result = ModelBindingResult.Success(filters);

            return Task.CompletedTask;
        }
    }

}
