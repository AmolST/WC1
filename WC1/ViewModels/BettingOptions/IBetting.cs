using System.Collections.Generic;

namespace WC1.ViewModels.BettingOptions
{
  //public interface IBetting
  //{
  //  int BetID { get; set; }
  //  string Question { get; set; }
  //  string ViewName { get; set; }
  //  string Prediction { get; set; }
  //  string BetValue { get; set; }
  //  int BetValueID { get; set; }
  //  int PredictionID { get; set; }
  //  int BetWeightage { get; set; }
  //}

  public class PredictionViewModel
  {
    public int FixtureID { get; set; }
    public int ProfileID { get; set; }
    //public Dictionary<string, string> PredictionDictonary { get; set; }
    public Dictionary<int, int> PredictionDictonary { get; set; }
  }

  public class Betting
  {
    public virtual int BetID { get; set; }
    public virtual string Question { get; set; }
    public virtual string ViewName { get; set; }
    public virtual string Prediction { get; set; }
    public virtual int BetValueID { get; set; }
    public virtual int PredictionID { get; set; }
    public virtual int BetWeightage { get; set; }
    public virtual bool Disabled { get; set; }
  }
}