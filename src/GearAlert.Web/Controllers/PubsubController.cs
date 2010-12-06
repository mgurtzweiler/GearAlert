using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GearAlert.Web.Models.Pubsub;

namespace GearAlert.Web.Controllers
{
    public class PubsubController : Controller
    {
        [HttpGet]
        public ActionResult Callback(HubChallengeInputModel input) {
            return View(input);
        }
    }
}
