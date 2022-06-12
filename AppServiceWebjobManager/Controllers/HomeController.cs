﻿using AppServiceWebjobManager.Models;
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
            //ViewBag.WebJobSettingItems = GetWebJobSettingItems();
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
        /// <summary>
        /// Run History
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        [Route("{WebJobSettingName}/TriggeredWebJob/{WebJobName}/History", Name = "TriggeredHistory")]
        public IActionResult History(string WebJobSettingName, string WebJobName)
        {
            SetWebJobSetting(WebJobSettingName);

            var model = _kuduService.GetWebJobHistory("Triggered", WebJobName).ToViewModel();
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