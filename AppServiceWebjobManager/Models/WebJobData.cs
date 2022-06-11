namespace AppServiceWebjobManager.Models
{
    public class WebJobData
    {
        public Latest_Run latest_run { get; set; }
        public string history_url { get; set; }
        public string scheduler_logs_url { get; set; }
        public string name { get; set; }
        public string run_command { get; set; }
        public string url { get; set; }
        public string extra_info_url { get; set; }
        public string type { get; set; }
        public object error { get; set; }
        public bool using_sdk { get; set; }
        public Settings settings { get; set; }

        public WebJobListViewModel ToViewModel()
        {
            return new WebJobListViewModel()
            {
                Name = name,
                Schedule = settings.schedule,
                Status = latest_run?.status,
                Type = type,
            };
        }
    }

    public class Latest_Run
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
    }

    public class Settings
    {
        public string schedule { get; set; }
    }
}
