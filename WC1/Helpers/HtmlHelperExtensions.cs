using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WC1.Helpers
{
  public static class HtmlHelperExtensions
  {
    public static MvcHtmlString Hyperlink(this HtmlHelper helper, string url, string linkTitle)
    {
      return MvcHtmlString.Create(string.Format("<a href='{0}' target='_blank'>{1}</a>", url, linkTitle));
    }
  }
}