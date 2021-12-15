using JobBoard.web.Models;
using JobBoard.Web.Models;

namespace JobBoard.web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
