using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Areas.Admin.Models;
using WC1.Models;
using WC1.ViewModels.BettingOptions;

namespace WC1.Services.Abstract
{
  public interface IResultService
  {
    FixtureResultViewModel GetResultViewModel();
    bool UpdateFixtureResult(BettingViewModel bettingViewModel);
  }
}