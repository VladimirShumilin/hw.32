using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApplication1.Models;
using MyWebApplication1.Models.Db;
using MyWebApplication1.Respositories.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyWebApplication1.Controllers
{
    public class UsersController : Controller
    {
        // ссылка на репозиторий
        private readonly IBlogRepository _repo;
        private readonly ILogger<HomeController> _logger;

        public UsersController(ILogger<HomeController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // Сделаем метод асинхронным
        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [HttpGet]
        public  async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
