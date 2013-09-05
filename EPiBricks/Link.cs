﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class Link : IEditHtmlString, ICustomizable
    {
        public Link()
        {
            Attributes = new Dictionary<string, object>();
        }

        public string Url { get; set; }

        public string Text { get; set; }

        public string Target { get; set; }

        public string Class { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public IHtmlString EditorStart { get; set; }

        public IHtmlString EditorEnd { get; set; }

        public IHtmlString DefaultValue { get; set; }

        public string ToHtmlString()
        {
            var linkHtml = ToHtml().IfNotNullOrEmpty() ?? DefaultValue.ToHtml();

            return string.Concat(EditorStart.ToHtml(), linkHtml, EditorEnd.ToHtml());
        }

        public string ToHtml()
        {
            if (Url.IsNullOrEmpty() && Text.IsNullOrEmpty())
            {
                return null;
            }

            TagBuilder tag;

            if (Url.IsNotNullOrEmpty())
            {
                tag = new TagBuilder("a");
                tag.Attributes.Add("href", Url);

                if (Target.IsNotNullOrEmpty() && Target != "_self")
                {
                    tag.Attributes.Add("target", Target);
                }
            }
            else
            {
                tag = new TagBuilder("span");
            }

            tag.SetInnerText(Text.IfNotNullOrEmpty() ?? Url);
            
            if (Class.IsNotNullOrEmpty())
            {
                tag.AddCssClass(Class);

                if (Attributes.ContainsKey("class"))
                {
                    var additonalClass = Convert.ToString(Attributes["class"], CultureInfo.InvariantCulture);
                    tag.AddCssClass(additonalClass);
                    Attributes.Remove("class");
                }
            }

            tag.MergeAttributes(Attributes);

            return tag.ToString();
        }
    }
}
