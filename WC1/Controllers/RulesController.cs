using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WC1.Models;

namespace WC1.Controllers
{
    public class RulesController : Controller
    {
      readonly WcDbContext wcDbContext = new WcDbContext();
        //
        // GET: /Rules/
        public ActionResult Index()
        {
            return View(wcDbContext.Rules);
        }
    }
}
