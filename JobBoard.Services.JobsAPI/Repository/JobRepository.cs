using AutoMapper;
using JobBoard.Services.JobsAPI.DBContexts;
using JobBoard.Services.JobsAPI.Models;
using JobBoard.Services.JobsAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JobBoard.Services.JobsAPI.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public JobRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<JobDto> CreateUpdateJob(JobDto jobDto)
        {
            Job job = _mapper.Map<JobDto, Job>(jobDto);
            if(job.JobId > 0)
            {
                _db.Jobs.Update(job);
            }
            else
            {
                _db.Jobs.Add(job); 
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Job, JobDto>(job);
        }

        public async Task<bool> DeleteJob(int jobId)
        {
            try
            {
                Job job = await _db.Jobs.FirstOrDefaultAsync(u => u.JobId == jobId);
                if (job == null)
                {
                    return false;
                }
                _db.Jobs.Remove(job);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public async Task<JobDto> GetJobById(int jobId)
        {
            Job job = await _db.Jobs.Where(x => x.JobId == jobId).FirstOrDefaultAsync();
            return _mapper.Map<JobDto>(job);
        }

        public async Task<IEnumerable<JobDto>> GetJobByUserId(string userId)
        {
            List<Job> jobList = await _db.Jobs.Where(x => x.UserId == userId).ToListAsync();
            return _mapper.Map<List<JobDto>>(jobList);
        }

        public async Task<IEnumerable<JobDto>> GetJobs()
        {
            List<Job> jobList = await _db.Jobs.ToListAsync();
            return _mapper.Map<List<JobDto>>(jobList);
        }
    }
}
