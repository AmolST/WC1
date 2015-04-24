using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WC1.Models;
using WC1.Services.Realisation;

namespace WC1.Controllers
{
  public class FeedReaderController : Controller
  {
    private readonly FeedReaderService feedReaderService = new FeedReaderService();
    // GET: /FeedReader/
    public ActionResult Index()
    {
      var model = feedReaderService.GetScoreFeed();
      return View(model);
    }
  }
}
