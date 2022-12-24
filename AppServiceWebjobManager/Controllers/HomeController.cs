using AppServiceWebjobManager.Models;
using AppServiceWebjobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewBag.WebJobSettingItems = GetWebJobSettingItems();
            }
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// List WebJobs
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}", Name = "WebJobs")]
        public IActionResult WebJobs(string WebJobSettingName)
        {
            SetWebJobSetting(WebJobSettingName);

            var model = _kuduService.GetWebJobs().Select(x => x.ToViewModel());
            return View(model);
        }

        #region WebJobDetail
        /// <summary>
        /// Continuous WebJob
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/ContinuousWebJob/{WebJobName}", Name = "ContinuousWebJob")]
        public IActionResult ContinuousWebJob(string WebJobSettingName, string WebJobName)
        {
            SetWebJobSetting(WebJobSettingName);

            var data = _kuduService.GetWebJobDetail("Continuous", WebJobName).ToViewModel();
            var model = new WebJobDetailViewModel()
            {
                Name = data.Name,
                Status = data.Status,
                Type = data.Type,
                Log = _kuduService.GetContinuousWebJobLog(data.Name)
            };

            return View(model);
        }
        /// <summary>
        /// Triggered WebJob
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/TriggeredWebJob/{WebJobName}", Name = "TriggeredWebJob")]
        public IActionResult TriggeredWebJob(string WebJobSettingName, string WebJobName)
        {
            SetWebJobSetting(WebJobSettingName);

            var model = _kuduService.GetTriggeredWebJobHistoryList(WebJobName).ToViewModel();
            return View(model);
        }
        #endregion WebJobDetail

        /// <summary>
        /// Run History
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <param name="WebJobName"></param>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/TriggeredWebJob/{WebJobName}/History/{id}", Name = "TriggeredWebJobHistory")]
        public IActionResult TriggeredWebJobHistory(string WebJobSettingName, string WebJobName, string id)
        {
            SetWebJobSetting(WebJobSettingName);

            var data = _kuduService.GetTriggeredWebJobHistoryDetail(WebJobName, id).ToViewModel();
            var model = new WebJobDetailViewModel()
            {
                Id = data.Id,
                Name = WebJobName,
                Type = "Triggered",
                Duration = data.Duration,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                Status = data.Status,
                Log = _kuduService.GetTriggereWebJobLog(WebJobName, id)
            };

            return View(model);
        }

        /// <summary>
        /// Excute WebJob
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <param name="WebJobName"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/{type}WebJob/{WebJobName}/Start", Name = "ExcuteContinuousWebJob")]
        [Route("{WebJobSettingName}/{type}WebJob/{WebJobName}/Run", Name = "ExcuteTriggeredWebJob")]
        public IActionResult ExcuteWebJob(string WebJobSettingName, string WebJobName, string Type, string Argments)
        {
            SetWebJobSetting(WebJobSettingName);

            var result = _kuduService.ExcuteWebJob(Type, WebJobName, Argments);
            TempData["Message"] = result;

            return RedirectToAction("WebJobs", new { WebJobSettingName = WebJobSettingName });
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
        /// <summary>
        /// Get WebJobSetting SelectListItem
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetWebJobSettingItems()
        {
            return _webJobSettings.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = Url.Action("WebJobs", new { WebJobSettingName = x.Name })
            });
        }
    }
}