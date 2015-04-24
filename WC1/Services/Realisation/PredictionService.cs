using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Areas.Profile.Models;
using WC1.Models;
using WC1.Services.Abstract;

namespace WC1.Services.Realisation
{
  public class PredictionService : IPredictionService
  {
    private readonly WcDbContext wcDbContext = new WcDbContext();
    readonly IBetInformationService bettingService = new BetInformationService();
    
    public IEnumerable<PredictionViewModel> GetAllPredictions(int profileID)
    {
      var predictions = GetPredictions(profileID);

      IList<PredictionViewModel> predictionViewModel = new List<PredictionViewModel>();

      var fixtureGroup = predictions.GroupBy(p => p.FixtureID).ToList();
      var uniqueFixtures = fixtureGroup.Select(grp => grp.First()).ToList();

      foreach (var item in uniqueFixtures)
      {
        var viewModel = new PredictionViewModel
          {
            FixtureID = item.FixtureID,
            FixtureDate = wcDbContext.Fixtures.Single(f => f.ID == item.FixtureID).Date,
            FirstTeam = (from team in wcDbContext.Teams
                         join fixture in wcDbContext.Fixtures on team.ID equals fixture.FirstTeamID
                         join prediction in wcDbContext.Predictions on fixture.ID equals prediction.FixtureID
                         where fixture.ID == item.FixtureID
                         select team.TeamName).First(),
            SecondTeam = (from team in wcDbContext.Teams
                          join fixture in wcDbContext.Fixtures on team.ID equals fixture.SecondTeamID
                          join prediction in wcDbContext.Predictions on fixture.ID equals prediction.FixtureID
                          where fixture.ID == item.FixtureID
                          select team.TeamName).First(),
            BetDetails = new List<BetDetailsViewModel>()
          };


        foreach (var subitem in predictions)
        {
          var weightage = bettingService.GetBetWeightage(subitem.FixtureID, subitem.BettingId);
          if (item.FixtureID == subitem.FixtureID)
          {
            var betDetailsViewModel = (from betItem in wcDbContext.BettingItems
                                       where betItem.ID == subitem.BettingId
                                       select new BetDetailsViewModel
                                         {
                                           BetItem = betItem.Item,
                                           //BetValue = subitem.BetValue,
                                           BetWeightage = weightage
                                         }).First();
            viewModel.TotalBet += weightage;
            viewModel.BetDetails.Add(betDetailsViewModel);
          }
        }

        predictionViewModel.Add(viewModel);
      }
     
      return predictionViewModel;
    }

    private IList<Prediction> GetPredictions(int profileID)
    {
      return wcDbContext.Predictions.Where(p => p.ProfileID == profileID)
                                    .OrderBy(item => item.FixtureID).ToList();
    }

    public int GetPredictionValue(int profileID, int fixtureID, int bettingID)
    {
      return (from p in wcDbContext.Predictions
              where p.ProfileID == profileID && p.FixtureID == fixtureID && p.BettingId == bettingID
              select p.BetValueID).SingleOrDefault();
    }
  }
}