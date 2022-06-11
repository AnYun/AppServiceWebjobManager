using AppServiceWebjobManager.Models;
using AppServiceWebjobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AppServiceWebjobManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly List<WebJobSetting> _webJobSettings;
        private readonly IKuduService _kuduService;

        public HomeController(IConfiguration configuration, IKuduService kuduService)
        {
            _configuration = configuration;
            _webJobSettings = _configuration.GetSection("WebJobSettings").Get<List<WebJobSetting>>();

            _kuduService = kuduService;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var model = _webJobSettings.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name });
            return View(model);
        }
        /// <summary>
        /// List WebJobs
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <returns></returns>
        [Route("WebJobs/{WebJobSettingName?}", Name = "WebJobs")]
        public IActionResult WebJobs(string WebJobSettingName)
        {
            SetWebJobSetting(WebJobSettingName);

            var model = _kuduService.GetWebJobs().Select(x => x.ToViewModel());
            return PartialView(model);
        }
        /// <summary>
        /// Run History
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/{Type}WebJob/{WebJobName}/History", Name = "History")]
        public IActionResult History(string WebJobSettingName, string Type, string WebJobName)
        {
            SetWebJobSetting(WebJobSettingName);

            var model = _kuduService.GetWebJobHistory(Type, WebJobName).ToViewModel();
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Get Single WebJobSetting
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <returns></returns>
        private void SetWebJobSetting(string WebJobSettingName)
        {
            var setting = _webJobSettings.Single(x => x.Name == WebJobSettingName);
            _kuduService.Initialize(setting);
        }
    }
}