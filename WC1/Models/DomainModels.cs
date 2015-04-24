using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace WC1.Models
{
  public class Profile
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
  }

  public class LoginDetail
  {
    public int ProfileID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int MobileOne { get; set; }
    public int MobileTwo { get; set; }
  }

  public class BettingItem
  {
    public int ID { get; set; }
    public string Item { get; set; }
    public int Weightage { get; set; }
  }

  public class BettingOption
  {
    public int ID { get; set; }
    public int FixtureID { get; set; }
    public int BetttingID { get; set; }
    public int Weightage { get; set; }
    public bool Manadatory { get; set; }
  }

  public class Team
  {
    public int ID { get; set; }
    public string TeamName { get; set; }
    public virtual List<Player> Players { get; set; } 
  }

  public class Player
  {
    public int ID { get; set; }
    public int TeamID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  public class FixtureResult
  {
    public int ID { get; set; }
    //Fixture ID
    public int FixtureID { get; set; }
    //Fixture Result Status
    public int FixtureStatusID { get; set; }
    //Team ID
    public int TossWon { get; set; }
    //Team ID
    public int MatchWon { get; set; }
    //Player ID
    public int ManOfMatch { get; set; }
  }

  public class FixtureStatus
  {
    public int ID { get; set; }
    public string Status { get; set; }
  }

  public class Fixture
  {
    public int ID { get; set; }
    public DateTime Date { get; set; }
    //Foreign Key from Team table
    public int FirstTeamID { get; set; }
    //Foreign Key from Team table
    public int SecondTeamID { get; set; }
    //Foreign Key from Result table
    public int ResultID { get; set; }
    //Foreign Key from Venue table
    public int VenueID { get; set; }
  }

  public class Venue
  {
    public int ID { get; set; }
    public string Location { get; set; }
  }

  public class Prediction
  {
    public int ID { get; set; }
    //Foreign Key from Fixture table
    public int FixtureID { get; set; }
    //Foreign Key from Profiles table
    public int ProfileID { get; set; }
    //Foreign Key from BettingItems table
    public int BettingId { get; set; }
    //[Required(ErrorMessage = "Please select any one country")]
    //public string BetValue { get; set; }
    public int BetValueID { get; set; }

    //public virtual List<Profile> Profiles { get; set; }
    //public virtual List<BettingItem> BettingItems { get; set; }
  }

  public class Rule
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
  }

  public class WcDbContext : DbContext
  {
    public DbSet<Team> Teams { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Fixture> Fixtures { get; set; }
    public DbSet<BettingItem> BettingItems { get; set; }
    public DbSet<Rule> Rules { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<BettingOption> BettingOptions { get; set; }
    public DbSet<FixtureResult> FixtureResults { get; set; }
    public DbSet<FixtureStatus> FixtureStatuses { get; set; }
  }
}