using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BL
{
    public class LogMiddlware
    {
        private readonly RequestDelegate _next;
        private static int counterPost = 0, counterPut = 0,
            counterDelete = 0, counterGet = 0;
        public LogMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                counterPost++;
                Log.Information($"POST request made {counterPost} times.");
            }
            else if (context.Request.Method == HttpMethods.Put)
            {
                counterPut++;
                Log.Information($"PUT request made {counterPut} times.");
            }
            else if (context.Request.Method == HttpMethods.Delete)
            {
                counterDelete++;
                Log.Information($"DELETE request made {counterDelete} times.");
            }
            else if (context.Request.Method == HttpMethods.Get)
            {
                counterGet++;
                Log.Information($"GET request made {counterGet} times.");
            }
            await _next(context);
            //private readonly RequestDelegate _next;
            //private readonly ILogger<LogMiddlware> _logger;

            //public LogMiddlware(RequestDelegate next, ILogger<LogMiddlware> logger)
            //{
            //    _next = next;
            //    _logger = logger;
            //}

            //public async Task Invoke(HttpContext context)
            //{
            //    var stopwatch = Stopwatch.StartNew();
            //    context.Response.OnStarting(() =>
            //    {
            //        stopwatch.Stop();
            //        var responseTimeForCompleteRequest = stopwatch.ElapsedMilliseconds;
            //        _logger.LogInformation($"Request [{context.Request.Method}] at [{context.Request.Path}] took {responseTimeForCompleteRequest} ms");
            //        return Task.CompletedTask;
            //    });

            //    await _next(context);
        }
    }
}

