using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Perks;

namespace EPiBricks
{
    public static class HtmlExtensions
    {
        public static string Image(this UrlHelper helper, Image image, int width = 0, int height = 0)
        {
            if (image == null || image.Url.IsNullOrEmpty())
            {
                return null;
            }

            if (width > 0 && height > 0)
            {
                image.Width = width;
                image.Height = height;
            }

            return string.Format("{0}{1}width={2}&height={3}",
                image.Url, image.Url.Contains('?') ? ':' : '?', image.Width, image.Height);
        }
    }
}
