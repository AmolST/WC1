using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Areas.Admin.Models;
using WC1.Models;
using WC1.Services.Abstract;
using WC1.ViewModels.BettingOptions;

namespace WC1.Services.Realisation
{
  public class ResultService : IResultService
  {
    private readonly WcDbContext wcDbContext = new WcDbContext();
    private readonly IFixtureService fixtureService =  new FixtureService();
    public FixtureResultViewModel GetResultViewModel()
    {
      var fixtureResultViewModel = new FixtureResultViewModel
        {
          Fixtures = fixtureService.GetAllFixtures()
        };
      return fixtureResultViewModel;
    }

    public bool UpdateFixtureResult(BettingViewModel bettingViewModel)
    {
      var sucess = true;
      var fixtureResult = wcDbContext.FixtureResults.SingleOrDefault(fr => fr.FixtureID == bettingViewModel.FixtureID);
      if (fixtureResult != null)
      {
        //fixtureResult.TossWon = wcDbContext.Teams.Single(t => t.TeamName == bettingViewModel.TossViewModel.Prediction).ID;
        //fixtureResult.ManOfMatch = wcDbContext.Teams.Single(t => t.TeamName == bettingViewModel.TossViewModel.Prediction).ID;

      }
      return sucess;
    }

    private int GetPlayerID(string playerFullName)
    {
      var any = wcDbContext.Teams.Any(t => t.Players.Any(p => string.Format("{0} {1}", p.FirstName, p.LastName) == playerFullName));

      //var model = from team in wcDbContext.Teams
      //            where team.Players.Single(p => string.Format("{0} {1}", p.FirstName, p.LastName) == playerFullName) 

      return 1;
    }
  }
}