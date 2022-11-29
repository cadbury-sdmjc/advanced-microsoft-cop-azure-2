using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace function_app;

public record QueueMessage(int Tenant, Guid Id, string Content)
{
    public const string QueueName = "demo-queue";
};

public static class HttpTrigger
{
    [FunctionName("HttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req,
        [Queue(QueueMessage.QueueName, Connection = "AdvancedMicrosoftCop")]
        IAsyncCollector<QueueMessage> collector,
        ILogger log)
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            await collector.AddAsync(new(i, Guid.NewGuid(), $"some message content goes in here!\r\nCreated at {DateTime.UtcNow:O}"));
        }

        return new NoContentResult();
    }
}