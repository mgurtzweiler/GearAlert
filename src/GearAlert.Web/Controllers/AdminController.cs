using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GearAlert.Messages.Commands;
using GearAlert.Web.Models.Admin;
using GearAlert.Web.Reporting;
using NServiceBus;

namespace GearAlert.Web.Controllers
{
    public class AdminController : Controller
    {
        public IBus Bus { get; set; }

        public AdminController(IBus bus)
        {
            Bus = bus;
        }

        //
        // GET: /Admin/
        [HttpGet]
        public ActionResult Index()
        {
            var data = new ReportingServiceClient().GetAllFeeds();
            return View(data);
        }

        [HttpGet]
        public ActionResult NewFeed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewFeed(AddNewFeedInput input) {
            if (ModelState.IsValid) {
                var command = Mapper.Map<AddNewFeedInput, CreateNewFeedCommand>(input);
                Bus.Send(command);
            }
            return RedirectToAction("CreatedNewFeed", input);
        }

        [HttpGet]
        public ActionResult CreatedNewFeed(AddNewFeedInput input) {
            return View();
        }

        [HttpPost]
        public ActionResult ActivateFeed(ActivateFeedInput input)
        {
            if (ModelState.IsValid) {
                var command = Mapper.Map<ActivateFeedInput, ActivateFeedCommand>(input);
                Bus.Send(command);
            }
            return RedirectToAction("ActivatedFeed");
        }

        [HttpGet]
        public ActionResult ActivatedFeed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeactivateFeed(DeactivateFeedInput input) {
            if (ModelState.IsValid) {
                var command = Mapper.Map<DeactivateFeedInput, DeactivateFeedCommand>(input);
                Bus.Send(command);
            }
            return RedirectToAction("ActivatedFeed");
        }

        [HttpGet]
        public ActionResult DeactivateFeed() {
            return View();
        }

        [HttpGet]
        public ActionResult NewAlert(Guid feedId)
        {
            var feed = new ReportingServiceClient().GetFeed(feedId);
            return View(feed);
        }

        [HttpPost]
        public ActionResult CreateNewAlert(CreateNewAlertInput input)
        {
            if (ModelState.IsValid) {
                var command = Mapper.Map<CreateNewAlertInput, RegisterNewAlertCommand>(input);
                Bus.Send(command);
            }
            return RedirectToAction("CreatedNewAlert", input);
        }

        [HttpGet]
        public ActionResult CreatedNewAlert() {
            return View();
        }
    }


}
