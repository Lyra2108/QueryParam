using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace QueryParam.ModelBinding;

public class FixedArrayModelBinder<T> : ArrayModelBinder<T>
{
    public FixedArrayModelBinder(IModelBinder elementBinder, ILoggerFactory loggerFactory) : base(elementBinder, loggerFactory)
    {
    }

    public FixedArrayModelBinder(IModelBinder elementBinder, ILoggerFactory loggerFactory, bool allowValidatingTopLevelNodes) : base(elementBinder, loggerFactory, allowValidatingTopLevelNodes)
    {
    }

    public FixedArrayModelBinder(IModelBinder elementBinder, ILoggerFactory loggerFactory, bool allowValidatingTopLevelNodes, MvcOptions mvcOptions) : base(elementBinder, loggerFactory, allowValidatingTopLevelNodes, mvcOptions)
    {
    }

    public override Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(default);
            return Task.CompletedTask;
        }

        return base.BindModelAsync(bindingContext);
    }
}