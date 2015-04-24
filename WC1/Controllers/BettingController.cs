using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WC1.ModelBinder;
using WC1.Services.Abstract;
using WC1.Services.Realisation;
using WC1.ViewModels.BettingOptions;

namespace WC1.Controllers
{
  public class BettingController : Controller
  {
    private readonly IBettingService bettingService = new BettingService();

    [ActionName("Index")]
    public ActionResult Index(int fixtureID)
    {
      const int profileID = 1;
      TempData["FixtureID"] = fixtureID;
      ViewBag.ReturnUrl = null;
      var model = bettingService.GetBetDetails(fixtureID, profileID);

      return View("BetView", model);
    }

    [HttpPost]
    [ActionName("Index")]
    public ActionResult SubmitBet([ModelBinder(typeof(BetModelBinder))] PredictionViewModel model, string returnUrl)
    {

      if (TempData.ContainsKey("FixtureID"))
      {
        model.FixtureID = (int)TempData["FixtureID"];
        model.ProfileID = 1;
        if (ModelState.IsValid)
        {
          bettingService.UpdatePredictionValue(model);
          return RedirectToLocal(returnUrl);
        }
      }

      ModelState.AddModelError("", "Unable to process bet");
      return View("BetView");
    }
    
    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Fixture");
    }
  }
}
