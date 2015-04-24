using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WC1.Services.Realisation;
using WC1.ViewModels;
using WC1.ViewModels.BettingOptions;

namespace WC1.Controllers
{
  public class FixtureController : Controller
  {
    private readonly FixtureService fixtureService = new FixtureService();
    // GET: /Fixtures/
    public ActionResult Index()
    {
      var model = fixtureService.GetAllFixtures();
      return View(model);
    }

    public ViewResult Details(int fixtureID)
    {
      ViewBag.FixtureID = fixtureID;
      var model = fixtureService.GetFixtureDetails(fixtureID);
      return View();
    }
    
  }
}
