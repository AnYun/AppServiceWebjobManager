using System.ComponentModel.DataAnnotations;

namespace AppServiceWebjobManager.Models
{
    /// <summary>
    /// WebJob History ViewModel
    /// </summary>
    public class WebJobHistoryViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        public string Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; }
        /// <summary>
        /// StartTime
        /// </summary>
        [Display(Name = "StartTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// EndTime
        /// </summary>
        [Display(Name = "EndTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        [Display(Name = "Duration")]
        public string Duration { get; set; }
    }
}
