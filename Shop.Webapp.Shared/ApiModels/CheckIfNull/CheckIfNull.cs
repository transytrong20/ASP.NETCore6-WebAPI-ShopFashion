using System.Reflection;

namespace Shop.Webapp.Shared.ApiModels.CheckIfNull
{
    public class ModelCheckIfNull<TModel, TDomain>
    {
        public static void CheckIfNull(TModel model, TDomain domainObject)
        {
            PropertyInfo[] properties = typeof(TModel).GetProperties();

            foreach (var property in properties)
            {
                var modelValue = property.GetValue(model);
                if (modelValue == null)
                {
                    var domainValue = typeof(TDomain).GetProperty(property.Name)?.GetValue(domainObject);
                    if (domainValue != null)
                    {
                        property.SetValue(model, domainValue);
                    }
                }
            }
        }
    }
}
