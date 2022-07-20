using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        //dynamic attribute for tag helper by creating a property member

        [HtmlAttributeName("asp-my-email")]
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{MyEmail}");
            output.Attributes.Add(new TagHelperAttribute("id", "my-email-id"));
            output.Attributes.Add("name", "email");
            output.Content.SetContent("my-email");
        }
    }
}
