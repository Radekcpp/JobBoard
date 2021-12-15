using System;
using System.Collections.Generic;
using JobBoard.Services.JobsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services.JobsAPI.DBContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Job> Jobs { get; set; }
    }
}
