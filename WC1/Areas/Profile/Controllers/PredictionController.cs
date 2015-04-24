using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WC1.Areas.Profile.Models;
using WC1.ModelBinder;
using WC1.Services.Realisation;

namespace WC1.Areas.Profile.Controllers
{
  public class PredictionController : Controller
  {
    private readonly BettingService bettingService = new BettingService();
    public ActionResult MyProfile(int profileID)
    {
      var model = bettingService.GetAllPredictions(profileID);
      ViewBag.ProfileName = "Amol";
      return model.Any() ? View(model) : View("BlankBet");
    }

    [HttpPost]
    public ActionResult MyProfile([ModelBinder(typeof(BetModelBinder))] ViewModels.BettingOptions.PredictionViewModel model)
    {
      model.ProfileID = 1;
      bettingService.UpdatePredictionValue(model);
      return RedirectToAction("MyProfile", new {profileId = 1});
    }
  }
}
