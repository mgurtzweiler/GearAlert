using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GearAlert.Messages.Commands;
using GearAlert.Web.Models.Home;
using NServiceBus;

namespace GearAlert.Web.Controllers
{
    public class HomeController : Controller
    {
        public IBus Bus { get; set; }

        public HomeController(IBus bus)
        {
            Bus = bus;
        }

        public ActionResult Index()
        {
            var data = new Reporting.ReportingServiceClient().GetAllFeeds();
            return View(data);
        }

        public ActionResult New(AddNewFeedInput index)
        {
            if(ModelState.IsValid)
            {
                var command = Mapper.Map<AddNewFeedInput, CreateNewFeedCommand>(index);
                Bus.Send(command);
            }
            return RedirectToAction("Index");
        }
    }
}
