// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace QueryParam.ModelBinding;

/// <summary>
/// An <see cref="IModelBinderProvider"/> for arrays.
/// </summary>
public class FixedArrayModelBinderProvider : IModelBinderProvider
{
    /// <inheritdoc />
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType.IsArray)
        {
            var elementType = context.Metadata.ElementMetadata!.ModelType;
            var binderType = typeof(FixedArrayModelBinder<>).MakeGenericType(elementType);
            var elementBinder = context.CreateBinder(context.Metadata.ElementMetadata);

            var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            var mvcOptions = context.Services.GetRequiredService<IOptions<MvcOptions>>().Value;
            return (IModelBinder)Activator.CreateInstance(
                binderType,
                elementBinder,
                loggerFactory,
                true /* allowValidatingTopLevelNodes */,
                mvcOptions)!;
        }

        return null;
    }
}
