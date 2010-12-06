using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GearAlert.Messages.Commands;
using GearAlert.Web.Models.Admin;
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
           
            return View();
        }

    }
}
