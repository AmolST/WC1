using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WC1.ViewModels.BettingOptions;

namespace WC1.ModelBinder
{
  public class BetModelBinder : DefaultModelBinder
  {
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      //TODO : Below code is became tightly coupled. Use some generic method to avoid it.
      if (bindingContext.ModelType.Name == typeof(PredictionViewModel).Name)
      {
        var predictionCollection = new Dictionary<int, int>();
        var authForm = controllerContext.HttpContext.Request.Form;

        var betIDs = new List<string>();
        var predictions = new List<string>();
        var fixtureID = 0;
        if (authForm["m.BetID"] != null)
        {
          betIDs = authForm["m.BetID"].Split(',').ToList();
          predictions = authForm["m.Prediction"].Split(',').ToList();
          fixtureID = Int32.Parse(authForm["FixtureID"]);
        }
        else
        {
          for (var index = 0; index < authForm.Count; index++)
          {
            predictions = authForm.GetValues(index).ToList();
            betIDs = authForm.GetValues(++index).ToList();
            fixtureID = Int32.Parse(authForm.GetValues(++index).First());
          }
        }
        
        //Add the retrieved Bet Id and Value in a collection
        if (betIDs.Count == predictions.Count)
        {
          for (var index = 0; index < betIDs.Count; index++)
          {
            var betID = Int32.Parse(betIDs[index]);
            var betValue = predictions[index].Equals(string.Empty) ? 0 : Int32.Parse(predictions[index]);
            predictionCollection.Add(betID, betValue);
          }
        }
        
        var predictionViewModel = new PredictionViewModel {PredictionDictonary = predictionCollection, FixtureID = fixtureID};

        return predictionViewModel;
      }

      return base.BindModel(controllerContext, bindingContext);
    }
  }
}