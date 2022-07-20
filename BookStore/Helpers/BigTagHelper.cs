using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class BigTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h3";
            output.Attributes.RemoveAll("big");
            output.Attributes.SetAttribute("class", "text-danger");
            output.PostContent.SetHtmlContent(" idriss");
            output.PostContent.SetHtmlContent(" ait si el arabi");
        }
    }
}
