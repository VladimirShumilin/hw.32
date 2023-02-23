using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using MyWebApplication1.Respositories.Interfaces;
using MyWebApplication1.Models.Db;

namespace MyWebApplication1.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        // ссылка на репозиторий
        private readonly ILogsRespository _repo;
        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, ILogsRespository repo)
        {
            _next = next;
            _repo = repo;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            //// Для логирования данных о запросе используем свойста объекта HttpContext
            //Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            LogConsole(context);
            //await LogFile(context);

            await LogToDatabase(context);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
        private static string LogDir =  System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "Logs";
        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");

        }

        private async Task LogFile(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)

            string logFilePath = Path.Combine(LogDir, "RequestLog.txt");

            if(!Directory.Exists(LogDir))
                Directory.CreateDirectory(LogDir);


            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
        private async Task LogToDatabase(HttpContext context)
        {
            // Строка для публикации в лог
            Request request = new Request() { Url = $" http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}" };

            await _repo.AddRequest(request);
        }
    }
}
