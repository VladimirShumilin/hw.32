using Microsoft.EntityFrameworkCore;
using MyWebApplication1.Models;
using MyWebApplication1.Models.Db;
using MyWebApplication1.Respositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyWebApplication1.Respositories
{
    public class LogsRespository : ILogsRespository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        // Метод-конструктор для инициализации
        public LogsRespository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddRequest(Request request)
        {
            request.Date = DateTime.Now;
            request.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }
    }
}
