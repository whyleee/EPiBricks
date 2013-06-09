using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPiGlue;
using Perks;

namespace EPiBricks
{
    public class Link : IEditHtmlString
    {
        public string Url { get; set; }

        public string Text { get; set; }

        public string Target { get; set; }

        public string Class { get; set; }

        public IHtmlString EditorStart { get; set; }

        public IHtmlString EditorEnd { get; set; }

        public IHtmlString DefaultValue { get; set; }

        public string ToHtmlString()
        {
            var linkHtml = ToHtml().IfNotNullOrEmpty() ?? DefaultValue.ToHtml();

            return string.Concat(EditorStart.ToHtml(), linkHtml, EditorEnd.ToHtml());
        }

        public string ToHtml(object htmlAttributes = null)
        {
            if (Url.IsNullOrEmpty())
            {
                return null;
            }

            var a = new TagBuilder("a");
            a.Attributes.Add("href", Url);
            a.SetInnerText(Text.IfNotNullOrEmpty() ?? Url);

            if (Target.IsNotNullOrEmpty() && Target != "_self")
            {
                a.Attributes.Add("target", Target);
            }
            if (Class.IsNotNullOrEmpty())
            {
                a.AddCssClass(Class);
            }

            a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return a.ToString();
        }
    }
}
