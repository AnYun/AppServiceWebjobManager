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
        /// <param name="WebJobName"></param>
        /// <returns></returns>
        public IActionResult ListWebJobs(string WebJobName)
        {
            var setting = _webJobSettings.Single(x => x.Name == WebJobName);
            _kuduService.Initialize(setting);
            var model = _kuduService.GetWebJobs().Select(x => x.ToViewModel());

            return PartialView(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}