using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FoodFlow.BuildingBlocks.API.ModelConventions;

public sealed partial class KebabCaseControllerRouteConvention : IControllerModelConvention
{
    private const string _placeHolder = "[controller]";

    public void Apply(ControllerModel controller)
    {
        var kebabCaseName = ToKebabCase(controller.ControllerName);

        foreach (var selector in controller.Selectors)
        {
            var route = selector.AttributeRouteModel;

            if (route?.Template is null)
            {
                continue;
            }

            route.Template = route.Template.Replace(
                _placeHolder,
                kebabCaseName,
                StringComparison.OrdinalIgnoreCase);
        }
    }

    private static string ToKebabCase(string value)
    {
        value = AbbreviationRegex().Replace(value, "$1-$2");
        value = PascalCaseRegex().Replace(value, "$1-$2");

        return value.ToLowerInvariant();
    }

    [GeneratedRegex(@"([A-Z]+)([A-Z][a-z])")]
    private static partial Regex AbbreviationRegex();

    [GeneratedRegex(@"([a-z0-9])([A-Z])")]
    private static partial Regex PascalCaseRegex();
}
