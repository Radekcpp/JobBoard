using JobBoard.web.Models;
using JobBoard.web.Services.IServices;
using JobBoard.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace JobBoard.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobService _jobService;

        public HomeController(ILogger<HomeController> logger, IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        public async Task<IActionResult> Index()
        {
            List<JobDto> list = new();
            var response = await _jobService.GetAllJobsAsync<ResponseDto>("");
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<JobDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> Details(int jobId)
        {
            JobDto model = new();
            var response = await _jobService.GetJobByIdAsync<ResponseDto>(jobId, "");
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<JobDto>(Convert.ToString(response.Result));
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}