using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EPiGlue;
using EPiServer.Editor;
using EPiServer.Web;
using EPiServer.Web.Mvc;
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

        public static bool InEditMode(this HtmlHelper html)
        {
            return html.ViewContext.InEditMode();
        }

        public static bool InEditMode(this ViewContext viewContext)
        {
            return GetContextMode(viewContext.RequestContext) == ContextMode.Edit;
        }

        public static EditableArea Editable<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, object>> expression)
        {
            var field = expression.Compile()(html.ViewData.Model) as IEditHtmlString;

            if (field != null)
            {
                html.ViewContext.Writer.Write(field.EditorStart);
            }

            return new EditableArea(html.ViewContext, field);
        }

        internal static void EndEditableArea(ViewContext viewContext)
        {
            if (viewContext.InEditMode())
            {
                ContentContext.PopCurrentProperty(viewContext.RequestContext);
                viewContext.Writer.Write("</div>");
            }
        }

        private static ContextMode GetContextMode(RequestContext requestContext)
        {
            object contextMode;
            if (requestContext.RouteData.DataTokens.TryGetValue("contextmode", out contextMode))
            {
                return (ContextMode)contextMode;
            }

            return PageEditing.PageIsInEditMode ? ContextMode.Edit : ContextMode.Default;
        }
    }
}
