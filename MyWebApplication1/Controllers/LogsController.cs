using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApplication1.Respositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication1.Controllers
{
    public class LogsController : Controller
    {
        // ссылка на репозиторий
        private readonly ILogsRespository _repo;
        private readonly ILogger<HomeController> _logger;

        public LogsController(ILogger<HomeController> logger, ILogsRespository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // Сделаем метод асинхронным
        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();
            
            return View(requests.OrderByDescending(x => x.Date));
        }
    }
}
