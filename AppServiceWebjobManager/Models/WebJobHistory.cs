namespace AppServiceWebjobManager.Models
{
    /// <summary>
    /// WebJobHistory
    /// </summary>
    public class WebJobHistory
    {
        public Run[] runs { get; set; }

        public List<WebJobHistoryViewModel> ToViewModel() => runs.Select(x => x.ToViewModel()).ToList();
    }

    public class Run
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string duration { get; set; }
        public string output_url { get; set; }
        public object error_url { get; set; }
        public string url { get; set; }
        public string job_name { get; set; }
        public string trigger { get; set; }

        public WebJobHistoryViewModel ToViewModel()
        {
            return new WebJobHistoryViewModel
            {
                Id = id,
                Status = status,
                StartTime = start_time,
                EndTime     = end_time,
                Duration = duration
            };
        }
    }

}
