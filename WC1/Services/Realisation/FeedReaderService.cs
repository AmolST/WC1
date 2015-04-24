using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using WC1.Models;
using WC1.Services.Abstract;

namespace WC1.Services.Realisation
{
  public class FeedReaderService : IFeedReaderService
  {
    public ScoreFeedModel GetScoreFeed()
    {
      //const string feedUrl = "http://www.ecb.co.uk/live-scores.xml";
      const string feedUrl = "http://static.cricinfo.com/rss/livescores.xml";
      var scoreFeedModel = new ScoreFeedModel();
      var client = new WebClient();
      var xmlData = client.DownloadString(feedUrl);
      XDocument xml = XDocument.Parse(xmlData);
      var feedModel = (from story in xml.Descendants("item")
                       select new FeedStructure
                       {
                         Title = (string)story.Element("title"),
                         Description = (string)story.Element("description"),
                         Link = (string)story.Element("link")
                       }).ToList();
      scoreFeedModel.Feeds = feedModel;
      return scoreFeedModel;
    }
  }
}