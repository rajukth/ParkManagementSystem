using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ParkManagementSystem.Web.Helper;

[HtmlTargetElement("label", Attributes = ForAttributeName)]
public class RequiredLabelTagHelper : TagHelper
{
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (For == null)
            return;

        var propertyInfo = For.Metadata.ContainerType?.GetProperty(For.Metadata.PropertyName!);
        if (propertyInfo != null)
        {
            var isRequired = propertyInfo.GetCustomAttribute<RequiredAttribute>() != null;

            if (isRequired)
            {
                // Append red asterisk
                output.Content.AppendHtml(" <span class='text-danger'>*</span>");
            }
        }
    }
}