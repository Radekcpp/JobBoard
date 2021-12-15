using JobBoard.Web.Models;

namespace JobBoard.web.Services.IServices
{
    public interface IJobService : IBaseService
    {
        Task<T> GetAllJobsAsync<T>(string token);
        Task<T> GetJobByIdAsync<T>(int id, string token);
        Task<T> CreateJobAsync<T>(JobDto jobDto, string token);
        Task<T> UpdateJobAsync<T>(JobDto jobDto, string token);
        Task<T> DeleteJobAsync<T>(int id, string token);
    }
}
