using JobBoard.web.Services.IServices;
using JobBoard.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobBoard.web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService) 
        { 
            _jobService = jobService;
        }
        public async Task<IActionResult> JobIndex()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<JobDto> list = new();
            var response = await _jobService.GetAllJobsAsync<ResponseDto>(accessToken);
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<JobDto>>(Convert.ToString(response.Result));
            }
            List<JobDto> actualList = new();
            int nameLength = User.Identity.Name.Length;
            foreach (var job in list)
            {
                int titleLenghtWithoutName = job.Title.Length - nameLength;
                if(job.Title.Substring(job.Title.Length-nameLength) == User.Identity.Name)
                {
                    job.Title = job.Title.Substring(0, titleLenghtWithoutName);
                    actualList.Add(job);
                }
            }
            return View(actualList);
        }
        public async Task<IActionResult> JobCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobCreate(JobDto model)
        {
            string name = User.Identity.Name;
            model.Title += name;
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _jobService.CreateJobAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(JobIndex));
                }
            }
            return View(model); 
        }
        public async Task<IActionResult> JobEdit(int jobId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _jobService.GetJobByIdAsync<ResponseDto>(jobId, accessToken);
            if (response != null && response.IsSuccess)
            {
                JobDto model = JsonConvert.DeserializeObject<JobDto>(Convert.ToString(response.Result));
                model.Title = DeleteNameFromTitle(model.Title);
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobEdit(JobDto model)
        {
            string name = User.Identity.Name;
            model.Title += name;
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _jobService.UpdateJobAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(JobIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> JobDelete(int jobId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _jobService.GetJobByIdAsync<ResponseDto>(jobId, accessToken);
            if (response != null && response.IsSuccess)
            {
                JobDto model = JsonConvert.DeserializeObject<JobDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobDelete(JobDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _jobService.DeleteJobAsync<ResponseDto>(model.JobId, accessToken);
                if (response!= null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(JobIndex));
                }
            }
            return View(model);
        }
        string DeleteNameFromTitle(string title)
        {
            int index = 0;
            bool previousIsLower = false;
            foreach (var letter in title)
            {
                if (previousIsLower && Char.IsUpper(letter))
                {
                    return title.Substring(0, index);
                }
                if (Char.IsLower(letter))
                {
                    previousIsLower = true;
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
