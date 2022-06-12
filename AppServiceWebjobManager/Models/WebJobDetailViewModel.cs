using System.ComponentModel.DataAnnotations;

namespace AppServiceWebjobManager.Models
{
    /// <summary>
    /// WebJobDetail ViewModel
    /// </summary>
    public class WebJobDetailViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        public string Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [Display(Name = "Type")]
        public string Type { get; set; }
        /// <summary>
        /// StartTime
        /// </summary>
        [Display(Name = "StartTime")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// EndTime
        /// </summary>
        [Display(Name = "EndTime")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        [Display(Name = "Duration")]
        public string? Duration { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; }
        /// <summary>
        /// Log
        /// </summary>
        [Display(Name = "Log")]
        public string Log { get; set; }
    }
}
