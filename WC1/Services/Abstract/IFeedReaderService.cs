using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WC1.Models;

namespace WC1.Services.Abstract
{
  public interface IFeedReaderService
  {
    ScoreFeedModel GetScoreFeed();
  }
}