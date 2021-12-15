using JobBoard.Services.JobsAPI.Models.Dto;

namespace JobBoard.Services.JobsAPI.Repository
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobDto>> GetJobs();
        Task<JobDto> GetJobById(int jobId);
        Task<JobDto> CreateUpdateJob(JobDto jobDto);
        Task<bool> DeleteJob(int jobId);
    }
}
