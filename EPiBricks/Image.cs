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
    public class Image : IEditHtmlString
    {
        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Alt { get; set; }

        public IHtmlString EditorStart { get; set; }

        public IHtmlString EditorEnd { get; set; }

        public IHtmlString DefaultValue { get; set; }

        public string ToHtmlString()
        {
            var imageHtml = ToHtml().IfNotNullOrEmpty() ?? DefaultValue.ToHtml();

            return string.Concat(EditorStart.ToHtml(), imageHtml, EditorEnd.ToHtml());
        }

        public string ToHtml(bool renderSize = false)
        {
            if (Url.IsNullOrEmpty())
            {
                return null;
            }

            var img = new TagBuilder("img");
            img.Attributes.Add("src", Url);

            if (Alt.IsNotNullOrEmpty())
            {
                img.Attributes.Add("alt", Alt);
            }

            if (renderSize && Width > 0 && Height > 0)
            {
                img.Attributes.Add("width", Width.ToString());
                img.Attributes.Add("height", Height.ToString());
            }

            return img.ToString(TagRenderMode.SelfClosing);
        }
    }
}
