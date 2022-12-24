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
        /// Get WebJob Detail
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        WebJobData GetWebJobDetail(string type, string name);
        /// <summary>
        /// Get Triggered WebJob History List
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        WebJobHistory GetTriggeredWebJobHistoryList(string name);
        /// <summary>
        /// Get Triggered WebJob History List
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <param name="id">Run id</param>
        /// <returns></returns>
        Run GetTriggeredWebJobHistoryDetail(string name, string id);
        /// <summary>
        ///  Get Triggere WebJob Log
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetTriggereWebJobLog(string name, string id);
        /// <summary>
        /// Get Continuous WebJob Log
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetContinuousWebJobLog(string name);
        /// <summary>
        /// Get WebJob Log
        /// </summary>
        /// <param name="url">log url</param>
        /// <returns></returns>
        string GetWebJobLog(string url);
        /// <summary>
        /// Run WebJob
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        string ExcuteWebJob(string type, string name, string arguments);
    }
}
