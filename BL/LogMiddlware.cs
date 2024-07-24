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
        private static int cntPost = 0, cntPut = 0,
            cntDelete = 0, cntrGet = 0;
        public LogMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                cntPost++;
                Log.Information($"POST request made {cntPost} times.");
            }
            else if (context.Request.Method == HttpMethods.Put)
            {
                cntPut++;
                Log.Information($"PUT request made {cntPut} times.");
            }
            else if (context.Request.Method == HttpMethods.Delete)
            {
                cntDelete++;
                Log.Information($"DELETE request made {cntDelete} times.");
            }
            else if (context.Request.Method == HttpMethods.Get)
            {
                cntrGet++;
                Log.Information($"GET request made {cntrGet} times.");
            }
            await _next(context);
           
        }
    }
}

