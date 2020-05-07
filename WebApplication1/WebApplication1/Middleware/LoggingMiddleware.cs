using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using tut6.Services;

namespace tut6.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IDbService dbSerivce)
        {
            string path = httpContext.Request.Path;
            string queryString = httpContext.Request?.QueryString.ToString();
            string method = httpContext.Request.Method.ToString();
            string bodyParameters = string.Empty;

            using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true))
            {
                bodyParameters = await reader.ReadToEndAsync();
            }

            FileStream LogWriter = new FileStream("requestsLog.txt", FileMode.Create);
            using (StreamWriter writer = new StreamWriter(LogWriter))
            {
                string text = $"Path: {path} \nQueryString:{queryString} \nMethod: {method} \nBody Parameters: {bodyParameters}";
                writer.WriteLine(text);
            }
            await _next(httpContext);
        }

    }
}
