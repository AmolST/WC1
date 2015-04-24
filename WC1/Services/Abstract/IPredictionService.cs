using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Areas.Profile.Models;

namespace WC1.Services.Abstract
{
  public interface IPredictionService
  {
    IEnumerable<PredictionViewModel> GetAllPredictions(int profileID);
    int GetPredictionValue(int profileID, int fixtureID, int bettingID);
  }
}