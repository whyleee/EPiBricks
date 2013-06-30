using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EPiGlue;
using Perks;
using Perks.Mvc;

namespace EPiBricks
{
    // NOTE: renders only as URL for now, without any HTML tags
    public class Video : IEditHtmlString, ICustomizable
    {
        public Video()
        {
            Attributes = new Dictionary<string, object>();
        }

        public string Url { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public IHtmlString EditorStart { get; set; }

        public IHtmlString EditorEnd { get; set; }

        public IHtmlString DefaultValue { get; set; }

        public string ToHtmlString()
        {
            var video = Url.IfNotNullOrEmpty() ?? DefaultValue.ToHtml();

            return string.Concat(EditorStart.ToHtml(), video, EditorEnd.ToHtml());
        }
    }
}
