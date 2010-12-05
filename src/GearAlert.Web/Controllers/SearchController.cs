using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GearAlert.Web.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query(string q)
        {
            var client = new Search.SearchServiceClient();
            var results = client.Query(q);
            return View(results);
        }
    }
}
