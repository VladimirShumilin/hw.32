using MyWebApplication1.Models.Db;
using System.Threading.Tasks;

namespace MyWebApplication1.Respositories.Interfaces
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
