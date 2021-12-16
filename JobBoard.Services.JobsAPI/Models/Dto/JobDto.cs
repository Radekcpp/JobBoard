using System.ComponentModel.DataAnnotations;

namespace JobBoard.Services.JobsAPI.Models.Dto
{
    public class JobDto
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string NiceToHave { get; set; }
        public string Duties { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
