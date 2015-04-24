using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Models;
using WC1.ViewModels.BettingOptions;
using WC1.ViewModels.Fixture;

namespace WC1.Areas.Admin.Models
{
  public class FixtureResultViewModel
  {
    public IEnumerable<FixtureViewModel> Fixtures { get; set; }
    public int FixtureID { get; set; }
    public BettingViewModel BettingViewModel { get; set; }
  }
}