using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;

namespace function_app;

public static class QueueTrigger
{
    [FunctionName("QueueTrigger")]
    public static async Task Run([QueueTrigger(QueueMessage.QueueName, Connection = "AdvancedMicrosoftCop")] QueueMessage message)
    {
        await Task.Delay(100 * Random.Shared.Next(5, 15));
        var containerName = "from-function-app";
        var miClient = new BlobContainerClient(new($"https://advmsftcop.blob.core.windows.net/{containerName}"), new DefaultAzureCredential());
        var blob = miClient.GetBlobClient($"{message.Tenant}/{message.Id}");
        await blob.UploadAsync(BinaryData.FromString(message.Content + $"\r\nWritten to blob at: {DateTime.UtcNow:O}"));
    }
}