using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace IncomeViewer.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Sortable(this HtmlHelper helper, string fieldName, string actionName, string linkText = null)
        {
            var getParams = helper.ViewContext.HttpContext.Request.QueryString;
            var direction = getParams["direction"];
            var css_class = fieldName == getParams["sort"] ? $"{direction}" : "";

            direction = fieldName == getParams["sort"] && getParams["direction"] == "asc" ? "desc" : "asc";
            
            var ret = MvcHtmlString.Create(
                helper.ActionLink(linkText ?? fieldName, actionName, new {sort = fieldName, direction}) + $"<div class='{css_class}'><div>");

            return ret;
        }
    }
}