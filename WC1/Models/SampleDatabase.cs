using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WC1.Models
{
  public class SampleDatabase : DropCreateDatabaseAlways<WcDbContext>
  {
    protected override void Seed(WcDbContext context)
    {
      new List<Team>
        {
          new Team
            {
              TeamName = "India",
              Players = new List<Player> {new Player {TeamID = 1, FirstName = "PlayerF1", LastName = "PlayerL1"},
                                          new Player {TeamID = 1, FirstName = "PlayerF12", LastName = "PlayerL12"},
                                          new Player {TeamID = 1, FirstName = "PlayerF13", LastName = "PlayerL13"}}
            },
          new Team
            {
              TeamName = "Pakistan",
              Players = new List<Player> {new Player {TeamID = 1, FirstName = "PlayerF21", LastName = "PlayerL21"},
                                          new Player {TeamID = 1, FirstName = "PlayerF22", LastName = "PlayerL22"},
                                          new Player {TeamID = 1, FirstName = "PlayerF23", LastName = "PlayerL23"}}
            },
          new Team
            {
              TeamName = "Ireland",
              Players = new List<Player> {new Player {TeamID = 1, FirstName = "PlayerF31", LastName = "PlayerL31"},
                                          new Player {TeamID = 1, FirstName = "PlayerF32", LastName = "PlayerL32"},
                                          new Player {TeamID = 1, FirstName = "PlayerF33", LastName = "PlayerL33"}}
            }
        }.ForEach(t => context.Teams.Add(t));

      new List<FixtureResult>
        {
          new FixtureResult {FixtureID = 1, FixtureStatusID = 1, TossWon = 1, MatchWon = 2},
          new FixtureResult {FixtureID = 2, FixtureStatusID = 1, TossWon = 1, MatchWon = 1}
        }.ForEach(fr => context.FixtureResults.Add(fr));

      new List<FixtureStatus>
        {
          new FixtureStatus {Status = "Won"},
          new FixtureStatus {Status = "Tie"},
          new FixtureStatus {Status = "Abandon"}
        }.ForEach(fs => context.FixtureStatuses.Add(fs));

      new List<Venue>
        {
          new Venue {Location = "Sydney"},
          new Venue {Location = "Melborne"},
          new Venue {Location = "Auckland"}
        }.ForEach(venue => context.Venues.Add(venue));

      new List<Fixture>
        {
          new Fixture {Date = new DateTime(2015, 2, 15), FirstTeamID = 1, SecondTeamID = 2, VenueID = 1},
          new Fixture {Date = new DateTime(2015, 2, 16), FirstTeamID = 1, SecondTeamID = 3, VenueID = 3},
          new Fixture {Date = new DateTime(2015, 3, 17), FirstTeamID = 2, SecondTeamID = 1, VenueID = 2},
          new Fixture {Date = new DateTime(2015, 4, 18), FirstTeamID = 3, SecondTeamID = 2, VenueID = 1},
        }.ForEach(fixture => context.Fixtures.Add(fixture));

      new List<BettingItem>
        {
          new BettingItem{Item = "Toss", Weightage = 10},
          new BettingItem{Item = "Winning Team", Weightage = 30},
          new BettingItem{Item = "Man of the Match", Weightage = 20},
        }.ForEach(item => context.BettingItems.Add(item));

      new List<BettingOption>
        {
          new BettingOption
            {
              FixtureID = 3, BetttingID = 1, Weightage = 50, Manadatory = true
            }
        }.ForEach(bItem => context.BettingOptions.Add(bItem));

      new List<Rule>
        {
          new Rule {Title = "Max amount", Description = "Max amount will be XXXX"},
          new Rule {Title = "Max bet placement", Description = "Player can place max 3 bet for single match"}
        }.ForEach(rule => context.Rules.Add(rule));

      

    }
  }
}