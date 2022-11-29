using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace function_app;

public record QueueMessage(int Tenant, string Content)
{
    public const string QueueName = "demo-queue";
};

public static class HttpTrigger
{
    [FunctionName("HttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
        [Queue(QueueMessage.QueueName)] IAsyncCollector<QueueMessage> collector,
        ILogger log)
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            await collector.AddAsync(new(i, $"{Guid.NewGuid()}"));
        }

        return new NoContentResult();
    }
}