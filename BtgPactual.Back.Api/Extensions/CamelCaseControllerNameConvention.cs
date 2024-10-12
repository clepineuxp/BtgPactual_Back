using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace BtgPactual.Back.Api.Extensions
{
    public class CamelCaseControllerNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerName = controller.ControllerName;
            controller.ControllerName = ToCamelCase(controllerName);
        }

        private string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
                return name;

            string camelCaseName = char.ToLowerInvariant(name[0]) + name.Substring(1);
            return camelCaseName;
        }
    }
}
