using System.Collections.Generic;
using WC1.ViewModels.Fixture;

namespace WC1.Areas.Profile.Models
{
  public class PredictionViewModel : FixtureViewModel
  {
    public List<BetDetailsViewModel> BetDetails { get; set; }
    public int TotalBet { get; set; }
    public bool CanEdit { get; set; }
  }

  public class BetDetailsViewModel
  {
    public string BetItem { get; set; }
    public string BetValue { get; set; }
    public int BetWeightage { get; set; }
  }


}