using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.ViewModels.BettingOptions;

namespace WC1.Services.Abstract
{
  public interface IBettingService
  {
    BettingViewModel GetBetDetails(int fixtureID, int profileID);
    void UpdatePredictionValue(PredictionViewModel model);
    IEnumerable<ProfileBettingViewModel> GetAllPredictions(int profileID);
  }
}