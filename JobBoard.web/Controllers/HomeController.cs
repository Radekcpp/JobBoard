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

        public async Task<IActionResult> Index(string search=null)
        {
            List<JobDto> list = new();
            var response = await _jobService.GetAllJobsAsync<ResponseDto>("");
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<JobDto>>(Convert.ToString(response.Result));
            }
            List<JobDto> actualList = new();
            if(search == null)
            {
                foreach(JobDto jobDto in list)
                {
                    jobDto.Title = DeleteNameFromTitle(jobDto.Title);
                }
                return View(list);
            }
            foreach (JobDto job in list)
            {
                if (job.Title.ToLower().Contains(search.ToLower()))
                {
                    actualList.Add(job); 
                }
            }
            foreach(JobDto job in actualList)
            {
                job.Title = DeleteNameFromTitle(job.Title);
            }
            return View(actualList);
        }
        public async Task<IActionResult> Details(int jobId)
        {
            JobDto model = new();
            var response = await _jobService.GetJobByIdAsync<ResponseDto>(jobId, "");
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<JobDto>(Convert.ToString(response.Result));
            }
            model.Title = DeleteNameFromTitle(model.Title);
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

        string DeleteNameFromTitle(string title)
        {
            int index = 0;
            bool previousIsLower = false;
            foreach(var letter in title)
            {
                if(previousIsLower && Char.IsUpper(letter))
                {
                    return title.Substring(0, index);
                }
                if (Char.IsLower(letter))
                {
                    previousIsLower= true;
                }
                else
                {
                    previousIsLower = false;
                }
                index++;
            }
            return title;
        }
    }
}