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
        /// Get WebJob History
        /// </summary>
        /// <param name="type">WebJob Type</param>
        /// <param name="name">WebJob Name</param>
        /// <returns></returns>
        public WebJobHistory GetWebJobHistory(string type, string name)
        {
            var request = new RestRequest($"api/{type}webjobs/{name}/history", Method.Get);
            SetBasicAuthorization(request);

            var response = this.restClient.ExecuteAsync(request).Result;

            if (string.IsNullOrEmpty(response.Content))
                return new WebJobHistory() { runs = new List<Run>() };

            var result = JsonSerializer.Deserialize<WebJobHistory>(response.Content);
            return result;
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
