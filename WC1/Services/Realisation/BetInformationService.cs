using System.Linq;
using WC1.Models;
using WC1.Services.Abstract;

namespace WC1.Services.Realisation
{
  public class BetInformationService : IBetInformationService
  {
    private readonly WcDbContext wcDbContext = new WcDbContext();

    public int GetBettingID(string bettingItem)
    {
      return wcDbContext.BettingItems.Single(betItem => betItem.Item.ToLower() == bettingItem.ToLower()).ID;
    }

    public int GetBetWeightage(int fixtureID, int bettingID)
    {
      //If Bet Option table do not have entry for Bet weightage then take default weightage
      var bettingModel = wcDbContext.BettingOptions.SingleOrDefault(betOp => betOp.FixtureID == fixtureID && betOp.BetttingID == bettingID);

      var weightage = bettingModel != null
                        ? bettingModel.Weightage
                        : wcDbContext.BettingItems.Single(betItem => betItem.ID == bettingID).Weightage;

      return weightage;
    }
  }
}