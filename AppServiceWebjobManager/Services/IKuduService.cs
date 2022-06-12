using AppServiceWebjobManager.Models;

namespace AppServiceWebjobManager.Services
{
    /// <summary>
    /// Kudu Service
    /// </summary>
    public interface IKuduService
    {
        /// <summary>
        /// Initialize KuduService
        /// </summary>
        /// <param name="webJobSetting"></param>
        void Initialize(WebJobSetting webJobSetting);
        /// <summary>
        /// Get WebJob List
        /// </summary>
        List<WebJobData> GetWebJobs();
        /// <summary>
        /// Get Triggered WebJob History List
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        WebJobHistory GetTriggeredWebJobHistoryList(string name);
    }
}
