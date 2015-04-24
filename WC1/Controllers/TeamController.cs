using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WC1.Models;

namespace WC1.Controllers
{
    public class TeamController : Controller
    {
        readonly WcDbContext dbContext = new WcDbContext();
        //
        // GET: /Team/
        public ActionResult Index()
        {
          return View(dbContext.Teams);
        }
    }
}
