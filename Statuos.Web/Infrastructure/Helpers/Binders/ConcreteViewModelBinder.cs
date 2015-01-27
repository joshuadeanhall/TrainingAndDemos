using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuos.Web.Infrastructure.Helpers.Binders
{
    public class ConcreteViewModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var typeValue = bindingContext.ValueProvider.GetValue("ConcreteModelType");
            dynamic model;
            Type type;
            if (string.IsNullOrEmpty(typeValue.AttemptedValue))
            {
                model = new BasicProjectViewModel();
                type = typeof(BasicProjectViewModel);
            }
            else
            {
                type = Type.GetType(
                    (string)typeValue.ConvertTo(typeof(string)),
                    true
                );
                model = Activator.CreateInstance(type);
            }
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, type);
            return model;
        }
    }
}