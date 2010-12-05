using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GearAlert.Web.Models.Home {
    public class AddNewFeedInput {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LandingUrl { get; set; }
        [Required]
        public string Url { get; set; }
    }
}