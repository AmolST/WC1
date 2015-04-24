using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WC1.ViewModels.BettingOptions
{
  public class MoMViewModel : Betting
  {
    public List<Player> Players { get; set; }
  }

  public class Player
  {
    public int ID { get; set; }
    public string Name { get; set; }
  }
}