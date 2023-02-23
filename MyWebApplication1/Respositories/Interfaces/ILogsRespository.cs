using MyWebApplication1.Models.Db;
using System.Threading.Tasks;

namespace MyWebApplication1.Respositories.Interfaces
{
    public interface ILogsRespository
    {
        Task AddRequest(Request request);
        Task<Request[]> GetRequests();
    }
}
