using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPiGlue;
using Perks;
using Perks.Mvc;

namespace EPiBricks
{
    public class Image : IEditHtmlString, ICustomizable
    {
        public Image()
        {
            Attributes = new Dictionary<string, object>();
        }

        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Alt { get; set; }

        public string Transform { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public IHtmlString EditorStart { get; set; }

        public IHtmlString EditorEnd { get; set; }

        public IHtmlString DefaultValue { get; set; }

        public string ToHtmlString()
        {
            var imageHtml = ToHtml().IfNotNullOrEmpty() ?? DefaultValue.ToHtml();

            return string.Concat(EditorStart.ToHtml(), imageHtml, EditorEnd.ToHtml());
        }

        public string ToHtml()
        {
            ParseAttributes();

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

            if (Width > 0 && Height > 0)
            {
                img.Attributes.Add("width", Width.ToString());
                img.Attributes.Add("height", Height.ToString());

                var src = img.Attributes["src"];
                img.Attributes["src"] = src + string.Format("{0}width={1}&height={2}",
                    src.Contains('?') ? ':' : '?', Width, Height);

                if (Transform.IsNotNullOrEmpty())
                {
                    img.Attributes["src"] = string.Format("{0}&transform={1}",
                        img.Attributes["src"], Transform);
                }
            }

            img.MergeAttributes(Attributes);

            return img.ToString(TagRenderMode.SelfClosing);
        }

        private void ParseAttributes()
        {
            foreach (var attr in Attributes)
            {
                var prop = GetType().GetProperty(attr.Key.ToTitleCase());

                if (prop != null)
                {
                    prop.SetValue(this, attr.Value, null);
                }
            }
        }
    }
}
