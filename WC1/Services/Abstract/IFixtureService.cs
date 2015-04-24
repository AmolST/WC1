using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.ViewModels;
using WC1.ViewModels.Fixture;

namespace WC1.Services.Abstract
{
  public interface IFixtureService
  {
    IEnumerable<FixtureViewModel> GetAllFixtures();
    FixtureViewModel GetFixtureDetails(int fixtureID);
  }
}