using JobBoard.Services.JobsAPI.Models.Dto;
using JobBoard.Services.JobsAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Services.JobsAPI.Controllers
{
    [Route("api/jobs")]
    public class JobsAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IJobRepository _jobRepository;

        public JobsAPIController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
            this._response = new ResponseDto();
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<JobDto> jobDtos = await _jobRepository.GetJobs();
                _response.Result = jobDtos;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                JobDto jobDto = await _jobRepository.GetJobById(id);
                _response.Result = jobDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize]

        public async Task<object> Post([FromBody] JobDto jobDto)
        {
            try
            {
                JobDto model = await _jobRepository.CreateUpdateJob(jobDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] JobDto jobDto)
        {
            try
            {
                JobDto model = await _jobRepository.CreateUpdateJob(jobDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _jobRepository.DeleteJob(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
