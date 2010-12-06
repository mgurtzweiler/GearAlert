using System.ComponentModel.DataAnnotations;

namespace GearAlert.Web.Models.Admin {
    public class AddNewFeedInput {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LandingUrl { get; set; }
        [Required]
        public string Url { get; set; }
    }
}