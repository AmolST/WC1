using System;

namespace WC1.ViewModels.Fixture
{
  public class FixtureViewModel
  {
    public int FixtureID { get; set; }
    public string FixtureName { get; set; }
    public DateTime FixtureDate { get; set; }
    public string FirstTeam { get; set; }
    public string SecondTeam { get; set; }
    public string Venue { get; set; }
    public string Result { get; set; }
  }
}