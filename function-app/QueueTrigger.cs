using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace function_app;

public static class QueueTrigger
{
    [FunctionName("QueueTrigger")]
    public static async Task Run([QueueTrigger(QueueMessage.QueueName)] QueueMessage message)
    {
        await Task.Delay(100 * Random.Shared.Next(5, 15));
        // write message contents to storage account
    }
}