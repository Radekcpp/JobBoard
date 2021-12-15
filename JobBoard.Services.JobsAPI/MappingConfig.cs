using AutoMapper;
using JobBoard.Services.JobsAPI.Models;
using JobBoard.Services.JobsAPI.Models.Dto;

namespace JobBoard.Services.JobsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<JobDto, Job>();
                config.CreateMap<Job, JobDto>();
            });

            return mappingConfig;
        }
    }
}
