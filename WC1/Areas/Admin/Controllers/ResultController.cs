using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WC1.Areas.Admin.Models;
using WC1.Services.Abstract;
using WC1.Services.Realisation;
using WC1.ViewModels.BettingOptions;

namespace WC1.Areas.Admin.Controllers
{
  public class ResultController : Controller
  {
    private readonly IResultService resultService = new ResultService();
    private readonly IBettingService bettingService = new BettingService();
    //
    // GET: /Admin/Result/
    public ActionResult Index()
    {
      var fixtureResultViewModel = resultService.GetResultViewModel();
      return View(fixtureResultViewModel);
    }

    [HttpPost]
    public ActionResult Index(BettingViewModel fixtureResultViewModel)
    {
      return View();
    }

    public ActionResult ShowBettingDetails(int fixtureID, int profileID)
    {
      var bettingViewModel = bettingService.GetBetDetails(fixtureID, profileID);
      return PartialView("ResultPartial", bettingViewModel);
    }

  }
}
