using JobBoard.web.Models;
using JobBoard.web.Services.IServices;
using JobBoard.Web.Models;

namespace JobBoard.web.Services
{
    public class JobService : BaseService, IJobService
    {
        private readonly IHttpClientFactory _clientFactory;
        public JobService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> CreateJobAsync<T>(JobDto jobDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = jobDto,
                Url = SD.JobsAPIBase + "/api/jobs",
                AccessToken = token
            });
        }

        public async Task<T> DeleteJobAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.JobsAPIBase + "/api/jobs/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllJobsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.JobsAPIBase + "/api/jobs",
                AccessToken = token
            });
        }

        public async Task<T> GetJobByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.JobsAPIBase + "/api/jobs/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateJobAsync<T>(JobDto jobDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = jobDto,
                Url = SD.JobsAPIBase + "/api/jobs",
                AccessToken = token
            });
        }
    }
}
