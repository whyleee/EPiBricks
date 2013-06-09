using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Perks;

namespace EPiBricks
{
    internal static class StringExtensions
    {
        public static string ToHtml(this IHtmlString htmlString)
        {
            return htmlString.IfNotNull(x => x.ToHtmlString());
        }
    }
}
