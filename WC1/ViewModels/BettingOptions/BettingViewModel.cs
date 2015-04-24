using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WC1.ViewModels.BettingOptions
{
  public class BettingViewModel
  {
    public int FixtureID { get; set; }
    public int ProfileID { get; set; }
    public List<Betting> AllViewModel { get; set; }
  }

  public class ProfileBettingViewModel : BettingViewModel
  {
    public List<string> Teams { get; set; }
    public DateTime FixtureDate { get; set; }
    public int TotalBet { get; set; }
    public bool Disabled { get; set; }
  }
}