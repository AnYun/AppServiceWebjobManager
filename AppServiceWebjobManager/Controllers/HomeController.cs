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
        /// Run History
        /// </summary>
        /// <param name="WebJobSettingName"></param>
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/ContinuousWebJob/{WebJobName}", Name = "ContinuousWebJob")]
        public IActionResult ContinuousWebJob()
        {
            return View();
        }
        /// <summary>
        /// Run History
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