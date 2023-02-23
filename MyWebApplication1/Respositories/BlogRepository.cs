using Microsoft.EntityFrameworkCore;
using MyWebApplication1.Models.Db;
using MyWebApplication1.Models;
using System.Threading.Tasks;
using System;
using MyWebApplication1.Respositories.Interfaces;

namespace MyWebApplication1.Respositories
{
    public class BlogRepository : IBlogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

       // public BlogRepository() { }
        // Метод-конструктор для инициализации
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
            // Получим всех активных пользователей
            return await _context.Users.ToArrayAsync();
        }
    }
}
