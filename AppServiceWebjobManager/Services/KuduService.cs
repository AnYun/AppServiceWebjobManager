using AppServiceWebjobManager.Models;
using RestSharp;
using System.Text.Json;

namespace AppServiceWebjobManager.Services
{
    /// <summary>
    /// Kudu Service
    /// </summary>
    public class KuduService : IKuduService
    {
        private WebJobSetting webJobSetting;
        private RestClient restClient;

        /// <summary>
        /// Initialize KuduService
        /// </summary>
        /// <param name="webJobSetting"></param>
        public void Initialize(WebJobSetting webJobSetting)
        {
            this.webJobSetting = webJobSetting;
            this.restClient = new RestClient(this.webJobSetting.KudoUrl);
        }
        /// <summary>
        /// Get WebJob List
        /// </summary>
        /// <returns></returns>
        public List<WebJobData> GetWebJobs()
        {
            var request = new RestRequest("api/webjobs", Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;
            var result = JsonSerializer.Deserialize<List<WebJobData>>(response.Content);

            return result;
        }
        /// <summary>
        /// Get WebJob Detail
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        public WebJobData GetWebJobDetail(string type, string name)
        {
            var request = new RestRequest($"api/{type}webjobs/{name}", Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;
            var result = JsonSerializer.Deserialize<WebJobData>(response.Content);

            return result;
        }
        /// <summary>
        /// Get WebJob History
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        public WebJobHistory GetTriggeredWebJobHistoryList(string name)
        {
            var request = new RestRequest($"api/triggeredwebjobs/{name}/history", Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;

            if (string.IsNullOrEmpty(response.Content))
                return new WebJobHistory() { runs = new List<Run>() };

            var result = JsonSerializer.Deserialize<WebJobHistory>(response.Content);
            return result;
        }
        /// <summary>
        /// Get Triggered WebJob History List
        /// </summary>
        /// <param name="name">WebJob Name</param>
        /// <param name="id">Run id</param>
        /// <returns></returns>
        public Run GetTriggeredWebJobHistoryDetail(string name, string id)
        {
            var request = new RestRequest($"api/triggeredwebjobs/{name}/history/{id}", Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;

            var result = JsonSerializer.Deserialize<Run>(response.Content);
            return result;
        }
        /// <summary>
        /// Get Continuous WebJob Log
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetContinuousWebJobLog(string name)
        {
            var url = $"vfs/data/jobs/continuous/{name}/job_log.txt";
            return GetWebJobLog(url);
        }
        /// <summary>
        ///  Get Triggere WebJob Log
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetTriggereWebJobLog(string name, string id)
        {
            var url = $"vfs/data/jobs/triggered/{name}/{id}/output_log.txt";
            return GetWebJobLog(url);
        }
        /// <summary>
        /// Get WebJob Log
        /// </summary>
        /// <param name="url">log url</param>
        /// <returns></returns>
        public string GetWebJobLog(string url)
        {
            var request = new RestRequest(url, Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;

            return response.Content;
        }

        /// <summary>
        /// Set  Basic Authorization
        /// </summary>
        /// <param name="request"></param>
        private void SetBasicAuthorization(RestRequest request)
        {
            request.AddHeader("Authorization", "Basic " + this.webJobSetting.BasicAuthorization);
        }

        
    }
}
