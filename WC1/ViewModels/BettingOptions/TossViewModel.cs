using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WC1.ViewModels.BettingOptions
{
  public class TossViewModel : Betting
  {
    public Dictionary<int, string> Teams { get; set; }
  }
}