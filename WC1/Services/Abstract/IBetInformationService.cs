namespace WC1.Services.Abstract
{
  public interface IBetInformationService
  {
    int GetBettingID(string bettingItem);
    int GetBetWeightage(int fixtureID, int bettingID);
  }
}