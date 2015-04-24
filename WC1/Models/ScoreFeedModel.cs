using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WC1.Models
{
  public class ScoreFeedModel
  {
    public IEnumerable<FeedStructure> Feeds { get; set; }
  }

  public class FeedStructure
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public string Guid { get; set; }
    public string Image { get; set; }
    public string PublishDate { get; set; }
  }
}