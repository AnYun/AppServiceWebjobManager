using System.ComponentModel.DataAnnotations;

namespace AppServiceWebjobManager.Models
{
    /// <summary>
    /// WebJobList ViewModel
    /// </summary>
    public class WebJobListViewModel
    {
        /// <summary>
        /// WebJob Name
        /// </summary>
        [Display(Name = "WebJob Name")]
        public string Name { get; set; }
        /// <summary>
        /// WebJob Type
        /// </summary>
        [Display(Name = "Type")]
        public string Type { get; set; }
        /// <summary>
        /// WebJob latest_run.status
        /// </summary>
        [Display(Name = "Latest Run Status")]
        public string Status { get; set; }
        /// <summary>
        /// WebJob Schedule
        /// </summary>
        [Display(Name = "Schedule")]
        public string Schedule { get; set; }
    }
}
