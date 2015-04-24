using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WC1.Models;
using WC1.Services.Abstract;
using WC1.ViewModels.BettingOptions;
using Player = WC1.ViewModels.BettingOptions.Player;

namespace WC1.Services.Realisation
{
  public class BettingService : IBettingService
  {
    private readonly WcDbContext wcDbContext = new WcDbContext();
    private readonly IPredictionService predictionService = new PredictionService();
    public readonly IBetInformationService BetInformationService = new BetInformationService();
    private ParticipatingTeamModel participatingTeamModel;

    public BettingViewModel GetBetDetails(int fixtureID, int profileID)
    {
      var bettingViewModelBase = new BettingViewModel
        {
          FixtureID = fixtureID,
          ProfileID = profileID,
          AllViewModel = GetAllViewModel(fixtureID, profileID)
        };

      return bettingViewModelBase;
    }

    private List<Betting> GetAllViewModel(int fixtureID, int profileID)
    {
      var viewModels = new List<Betting>();
      var tosVM = GetTossModel(fixtureID, profileID);
      var momVM = GetMoMModel(fixtureID, profileID);
      viewModels.Add(tosVM);
      viewModels.Add(momVM);

      return viewModels;
    }

    private ProfileBettingViewModel GetAdvanceBetDetails(int fixtureID, int profileID)
    {
      participatingTeamModel = ParticipatingTeams(fixtureID);
      var advanceBettingModel = new ProfileBettingViewModel
        {
          FixtureID = fixtureID,
          ProfileID = profileID,
          FixtureDate = wcDbContext.Fixtures.Single(f => f.ID == fixtureID).Date,
          Teams = new List<string>
            {
              wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.FirstTeamId).TeamName ,
              wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.SecondTeamId).TeamName 
            },
          AllViewModel = GetAllViewModel(fixtureID, profileID)
        };
      advanceBettingModel.TotalBet = advanceBettingModel.AllViewModel.Sum(betting => betting.BetWeightage);
      advanceBettingModel.Disabled = advanceBettingModel.AllViewModel.First().Disabled;
      
      return advanceBettingModel;
    }

    public void UpdatePredictionValue(PredictionViewModel model)
    {

      foreach (var prediction in model.PredictionDictonary)
      {
        var momModel = (from p in wcDbContext.Predictions
                        where p.FixtureID == model.FixtureID &&
                              p.ProfileID == model.ProfileID &&
                              p.BettingId == prediction.Key
                        select p).SingleOrDefault();
        if (momModel != null)
        {
          if (prediction.Value == 0)
          {
            //Remove from Table
            wcDbContext.Predictions.Remove(momModel);
          }
          else
          {
            //Update in the Table
            momModel.BetValueID = prediction.Value;
          }
        }
        else
        {
          if (prediction.Value != 0)
          {
            //Add new row in Table
            wcDbContext.Predictions.Add(new Prediction
              {
                FixtureID = model.FixtureID,
                ProfileID = model.ProfileID,
                BettingId = prediction.Key,
                BetValueID = prediction.Value
              });
          }
        }
      }
      wcDbContext.SaveChanges();
    }

    public IEnumerable<ProfileBettingViewModel> GetAllPredictions(int profileID)
    {
      var predictions = wcDbContext.Predictions.Where(p => p.ProfileID == profileID).OrderBy(item => item.FixtureID);
      var fixtureGroup = predictions.GroupBy(p => p.FixtureID).ToList();
      var uniqueFixtures = fixtureGroup.Select(grp => grp.First()).ToList();
      return uniqueFixtures.Select(fixture => GetAdvanceBetDetails(fixture.FixtureID, profileID)).ToList();
    }
    
    private ParticipatingTeamModel ParticipatingTeams(int fixtureID)
    {
      var model = from fixture in wcDbContext.Fixtures
                  where fixture.ID == fixtureID
                  select new ParticipatingTeamModel
                  {
                    FirstTeamId = fixture.FirstTeamID,
                    SecondTeamId = fixture.SecondTeamID
                  };
      //TODO : How to avoid First() operation on return value
      return model.First();
    }
    
    private TossViewModel GetTossModel(int fixtureID, int profileID)
    {
      const string BetItemName = "Toss";
      var tossViewModel = new TossViewModel();
      participatingTeamModel = ParticipatingTeams(fixtureID);
      
      tossViewModel.Teams = new Dictionary<int, string>
        {
          {0, "No Option"},
          {participatingTeamModel.FirstTeamId, wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.FirstTeamId).TeamName},
          {participatingTeamModel.SecondTeamId, wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.SecondTeamId).TeamName}
        };


      tossViewModel.BetID = wcDbContext.BettingItems.Single(betItem => betItem.Item == BetItemName).ID;
      tossViewModel.Question = "Who will win TOSS ?";
      tossViewModel.ViewName = "_TossPartialView1";
      tossViewModel.BetValueID = predictionService.GetPredictionValue(profileID, fixtureID, BetInformationService.GetBettingID(BetItemName));
      tossViewModel.BetWeightage = BetInformationService.GetBetWeightage(fixtureID, BetInformationService.GetBettingID(BetItemName));
      var dateTime = wcDbContext.Fixtures.Single(f => f.ID == fixtureID).Date;
      tossViewModel.Disabled = DateTime.Compare(dateTime, DateTime.Now.AddHours(4.0)) < 0;
      return tossViewModel;
    }

    private MoMViewModel GetMoMModel(int fixtureID, int profileID)
    {
      const string BetItemName = "Man of the Match";
      var momViewModel = new MoMViewModel();
      participatingTeamModel = ParticipatingTeams(fixtureID);
      
      momViewModel.Players = new List<Player>();
      wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.FirstTeamId)
                 .Players.ForEach(
                   p =>
                   momViewModel.Players.Add(new Player
                     {
                       ID = p.ID,
                       Name = string.Format("{0} {1}", p.FirstName, p.LastName)
                     }));

      wcDbContext.Teams.Single(team => team.ID == participatingTeamModel.SecondTeamId)
                 .Players.ForEach(
                   p =>
                   momViewModel.Players.Add(new Player
                   {
                     ID = p.ID,
                     Name = string.Format("{0} {1}", p.FirstName, p.LastName)
                   }));

                                         

      momViewModel.BetID = wcDbContext.BettingItems.Single(betItem => betItem.Item == BetItemName).ID;
      momViewModel.Question = "Who will be Man of the Match?";
      momViewModel.ViewName = "_ManOfMatchPartialView1";
      momViewModel.BetValueID = predictionService.GetPredictionValue(profileID, fixtureID, BetInformationService.GetBettingID(BetItemName));
      momViewModel.BetWeightage = BetInformationService.GetBetWeightage(fixtureID, BetInformationService.GetBettingID(BetItemName));
      var dateTime = wcDbContext.Fixtures.Single(f => f.ID == fixtureID).Date;
      momViewModel.Disabled = DateTime.Compare(dateTime, DateTime.Now.AddHours(4.0)) < 0;
      return momViewModel;
    }
    
  }
}