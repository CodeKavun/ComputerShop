using ComputerShopApp.Infrostructures.ModelBinders;
using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComputerShopApp.Infrostructures.ModelBinderProviders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            IHttpContextAccessor contextAccessor = context.Services.GetRequiredService<IHttpContextAccessor>();
            return context.Metadata.ModelType == typeof(Cart) ? new CartModelBinder(contextAccessor) : null;
        }
    }
}
