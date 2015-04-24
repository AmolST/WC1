using System.Collections.Generic;
using System.Linq;
using WC1.Models;
using WC1.Services.Abstract;
using WC1.ViewModels.Fixture;

namespace WC1.Services.Realisation
{
  public class FixtureService : IFixtureService
  {
    private readonly WcDbContext wcDbContext = new WcDbContext();

    public IEnumerable<FixtureViewModel> GetAllFixtures()
    {
      var fixturesViewModel = new List<FixtureViewModel>();
      //Note : Dont refactor to lambda expression. Getting error !
      foreach (var fixture in wcDbContext.Fixtures)
      {
        fixturesViewModel.Add(new FixtureViewModel
        {
          FixtureID = fixture.ID,
          FixtureName = string.Format("[{0}] {1} vs {2}", 
                                       fixture.Date.ToShortDateString(), 
                                       wcDbContext.Teams.Single(team => team.ID == fixture.FirstTeamID).TeamName, 
                                       wcDbContext.Teams.Single(team => team.ID == fixture.SecondTeamID).TeamName),
          //Date = string.Format("{0:dd-MMM-yyyy}", fixture.Date),
          FixtureDate = fixture.Date,
          FirstTeam = wcDbContext.Teams.Single(team => team.ID == fixture.FirstTeamID).TeamName,
          SecondTeam = wcDbContext.Teams.Single(team => team.ID == fixture.SecondTeamID).TeamName,
          Result = GetFixtureResult(fixture.ID),
          Venue = wcDbContext.Venues.Single(venue => venue.ID == fixture.VenueID).Location
        });
      }
      return fixturesViewModel;
    }

    public FixtureViewModel GetFixtureDetails(int fixtureID)
    {
      return null;
    }

    private string GetFixtureResult(int fixtureID)
    {
      var fResult = "Yet to Play";
      var fixtureResult = wcDbContext.FixtureResults.SingleOrDefault(fr => fr.FixtureID == fixtureID);
      if (fixtureResult != null)
      {
        var fixtureStatusID = fixtureResult.FixtureStatusID;
        var result = wcDbContext.FixtureStatuses.Single(fs => fs.ID == fixtureStatusID).Status;
        if (result == "Won")
        {
          var teamID = wcDbContext.FixtureResults.Single(fr => fr.FixtureID == fixtureID).MatchWon;
          fResult = string.Format("{0} Won", wcDbContext.Teams.Single(t => t.ID == teamID).TeamName);
        }
        else
        {
          fResult = result;
        }
      }
      return fResult;
    }
  }
}